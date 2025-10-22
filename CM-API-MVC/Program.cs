using CM_API_MVC.Contexts;
using CM_API_MVC.Dtos.Filial;
using CM_API_MVC.Dtos.Moto;
using CM_API_MVC.Dtos.Patio;
using CM_API_MVC.Repositories;
using CM_API_MVC.Settings;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

//Parte para testar com o banco de dados local
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
    new MySqlServerVersion(new Version(8, 0, 36))));

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseMySql(
//        Environment.GetEnvironmentVariable("MYSQL_CONNECTION") ?? builder.Configuration.GetConnectionString("MYSQL_CONNECTION"),
//        new MySqlServerVersion(new Version(8, 0, 36))
//    ));


//builder.Services.AddDbContext<AppDbContext>(options =>
//   options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));



builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<PatioRepository>();
builder.Services.AddScoped<FilialRepository>();
builder.Services.AddScoped<WifiRepository>();
builder.Services.AddScoped<MotoRepository>();
builder.Services.AddScoped<IotRepository>();
builder.Services.AddScoped<FilialLinksHelper>();
builder.Services.AddScoped<PatioLinksHelper>();
builder.Services.AddScoped<MotoLinksHelper>();


// Parte para testar com o banco de dados local
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("ConnectionStrings"));

//builder.Services.AddSingleton<IMongoClient>(sp =>
//{
//    var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI")
//                           ?? builder.Configuration.GetConnectionString("MONGODB_URI");

//    if (string.IsNullOrEmpty(connectionString))
//    {
//        throw new InvalidOperationException("MongoDB connection string is not configured.");
//    }

//    return new MongoClient(connectionString);
//});


builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("mottuDB");
});


builder.Services.AddSingleton<RegistroSinalRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

// Configura��o de autentica��o
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "EX: 'Bearer {token}'",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
            },
            Array.Empty<string>()
        }
    });
});


// Health Check
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy("API OK"))
    .AddMongoDb(sp =>
    {
        var cs = builder.Configuration["ConnectionStrings:MongoConnection"];
        return new MongoClient(cs);
    },
    name: "mongodb",
    timeout: TimeSpan.FromSeconds(10),
    tags: new[] { "db", "mongo" }
    );

builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ApiMongo"))
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddJaegerExporter(o =>
        {
            o.AgentHost = builder.Configuration["Jeager:Host"] ?? "localhost";
            o.AgentPort = int.TryParse(builder.Configuration["Jeager:Port"], out var port) ? port : 6831;
        })
        .AddConsoleExporter();
    });


// versionamento
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //db.Database.Migrate();
}

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwagger();

app.UseSwaggerUI(options =>
{
    foreach (var desc in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName.ToUpperInvariant());
    }
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowFrontend");

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

//dotnet publish -c Release -o ./publicado

//dotnet ef database update

//dotnet ef migrations script -o script.sql
