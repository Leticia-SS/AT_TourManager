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
                .WithMany(d => d.PacotesTuristicos)
                .UsingEntity<Dictionary<string, object>>(
                    "DestinoPacoteTuristico",
                    j => j.HasOne<Destino>().WithMany().HasForeignKey("DestinoId"),
                    j => j.HasOne<PacoteTuristico>().WithMany().HasForeignKey("PacoteTuristicoId"),
                    j => j.ToTable("DestinoPacoteTuristico"));

            modelBuilder.Entity<PacoteTuristico>()
                .Property(p => p.Preco)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.ClienteId);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.PacoteTuristico)
                .WithMany()
                .HasForeignKey(r => r.PacoteTuristicoId);
            
            modelBuilder.Entity<PaisDestino>()
                .HasMany(p => p.Destinos)
                .WithOne(d => d.PaisDestino)
                .HasForeignKey(d => d.PaisDestinoId);

            modelBuilder.Entity<Reserva>().HasQueryFilter(r => !r.IsDeleted);
        }


    }
    
    
}
