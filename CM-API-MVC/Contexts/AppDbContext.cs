using CM_API_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CM_API_MVC.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets (nomes no plural de boa prática)
        public DbSet<Filial> Filiais { get; set; }
        public DbSet<Patio> Patios { get; set; }
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Rfid> Rfids { get; set; }
        public DbSet<PosicaoMoto> PosicoesMotos { get; set; }
        public DbSet<ReceptorWifi> ReceptoresWifi { get; set; }
        public DbSet<DispositivoIot> DispositivosIot { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento 1:N -> Filial : Patio
            modelBuilder.Entity<Patio>()
                .HasOne(p => p.Filial)
                .WithMany(f => f.Patios)
                .HasForeignKey(p => p.IdFilial)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento 1:N -> Patio : Posicoes
            modelBuilder.Entity<PosicaoMoto>()
                .HasOne(pm => pm.Patio)
                .WithMany(p => p.PosicaoMoto)
                .HasForeignKey(pm => pm.IdPatio)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento 1:N -> Dispositivo : Posicoes
            modelBuilder.Entity<PosicaoMoto>()
                .HasOne(pm => pm.DispositivoIot)
                .WithMany(d => d.Posicoes)
                .HasForeignKey(pm => pm.IdDispositivo)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento 1:N -> Patio : ReceptorWifi
            modelBuilder.Entity<ReceptorWifi>()
                .HasOne(r => r.Patio)
                .WithMany(p => p.ReceptorWifi)
                .HasForeignKey(r => r.IdPatio)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento 1:1 -> DispositivoIot : Moto
            modelBuilder.Entity<DispositivoIot>()
                .HasOne(d => d.Moto)
                .WithOne()
                .HasForeignKey<DispositivoIot>(d => d.IdMoto)
                .OnDelete(DeleteBehavior.SetNull);


            // Relacionamento 1:1 -> RFID : Moto
            modelBuilder.Entity<Moto>()
                .HasOne(m => m.Rfid)
                .WithOne()
                .HasForeignKey<Moto>(m => m.CodTag)
                .OnDelete(DeleteBehavior.SetNull);


            base.OnModelCreating(modelBuilder);
        }
    }
}
