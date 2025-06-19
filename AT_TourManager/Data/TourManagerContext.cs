using AT_TourManager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AT_TourManager.Data
{
    public class TourManagerContext : DbContext
    {
        public TourManagerContext(DbContextOptions<TourManagerContext> options)
            : base(options)
        {
        }
        public DbSet<Models.PaisDestino> PaisesDestinos { get; set; }
        public DbSet<Models.Destino> Destinos { get; set; }
        public DbSet<Models.Cliente> Clientes { get; set; }
        public DbSet<Models.PacoteTuristico> PacotesTuristicos { get; set; }
        public DbSet<Models.Reserva> Reservas { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PacoteTuristico>()
                .HasMany(p => p.Destinos)
                .WithMany();

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.ClienteId);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.PacoteTuristico)
                .WithMany()
                .HasForeignKey(r => r.PacoteTuristicoId);
        }
    }
    
    
}
