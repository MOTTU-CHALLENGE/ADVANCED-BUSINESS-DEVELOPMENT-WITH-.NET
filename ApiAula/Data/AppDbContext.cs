using ApiAula.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiAula.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) 
        { 
        }
        public DbSet<Moto> Motos { get; set; }
    }
        
    }
