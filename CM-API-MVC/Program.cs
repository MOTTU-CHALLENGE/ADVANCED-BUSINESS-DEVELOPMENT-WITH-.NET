using CM_API_MVC.Contexts;
using CM_API_MVC.Dtos.Filial;
using CM_API_MVC.Dtos.Moto;
using CM_API_MVC.Dtos.Patio;
using CM_API_MVC.Repositories;
using CM_API_MVC.Settings;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
//    new MySqlServerVersion(new Version(8, 0, 36))));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        Environment.GetEnvironmentVariable("MYSQL_CONNECTION") ?? builder.Configuration.GetConnectionString("MYSQL_CONNECTION"),
        new MySqlServerVersion(new Version(8, 0, 36))
    ));


builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<PatioRepository>();
builder.Services.AddScoped<FilialRepository>();
builder.Services.AddScoped<WifiRepository>();
builder.Services.AddScoped<MotoRepository>();
builder.Services.AddScoped<IotRepository>();
builder.Services.AddScoped<FilialLinksHelper>();
builder.Services.AddScoped<PatioLinksHelper>();
builder.Services.AddScoped<MotoLinksHelper>();


//builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI")
                           ?? builder.Configuration.GetConnectionString("MONGODB_URI");

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("MongoDB connection string is not configured.");
    }

    return new MongoClient(connectionString);
});


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


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});



app.Run();

//dotnet publish -c Release -o ./publicado

//dotnet ef database update
