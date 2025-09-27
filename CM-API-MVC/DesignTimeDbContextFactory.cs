// Crie esse arquivo: DesignTimeDbContextFactory.cs

using CM_API_MVC.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CM_API_MVC
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<AppDbContext>();

            var connectionString = configuration.GetConnectionString("MySqlConnection");
            builder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)));

            return new AppDbContext(builder.Options);
        }
    }
}
