using CM_API_MVC.Contexts;
using CM_API_MVC.Repositories;
using CM_API_MVC.Settings;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
    new MySqlServerVersion(new Version(8, 0, 36))));

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<PatioRepository>();
builder.Services.AddScoped<FilialRepository>();
builder.Services.AddScoped<WifiRepository>();
builder.Services.AddScoped<MotoRepository>();
builder.Services.AddScoped<IotRepository>();


builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = builder.Configuration.GetSection("ConnectionStrings");
    var connectionString = settings["MongoConnection"];
    return new MongoClient(connectionString);
});

builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("mottuDB");
});


builder.Services.AddSingleton<RegistroSinalRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
