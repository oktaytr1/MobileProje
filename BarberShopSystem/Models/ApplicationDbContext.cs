using Microsoft.EntityFrameworkCore;

namespace BarberShopSystem.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        /*public DbSet<Cupo> Cupos { get; set; }*/
        public DbSet<Profesyonel> Profesyoneller { get; set; }
        public DbSet<Servis> Servisler { get; set; }
        public DbSet<ProfesyonelServis> ProfesyonelServisler { get; set; }
        public DbSet<Rezervasyon> Rezervasyonlar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relaciones para ProfesionalServis
            modelBuilder.Entity<ProfesyonelServis>()
                .HasKey(ps => new { ps.ProfesyonelId, ps.ServisId });

            modelBuilder.Entity<ProfesyonelServis>()
                .HasOne(ps => ps.Profesyonel)
                .WithMany(p => p.ProfesyonelServisler)
                .HasForeignKey(ps => ps.ProfesyonelId);

            modelBuilder.Entity<ProfesyonelServis>()
                .HasOne(ps => ps.Servis)
                .WithMany(s => s.ProfesyonelServisler)
                .HasForeignKey(ps => ps.ServisId);

            // Relaciones para Rezervasyon
            modelBuilder.Entity<Rezervasyon>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rezervasyonlar)
                .HasForeignKey(r => r.CustomerId);
            /*
            modelBuilder.Entity<Rezervasyon>()
                .HasOne(r => r.Cupo)
                .WithOne(c => c.Rezervasyon)
                .HasForeignKey<Rezervasyon>(r => r.CupoId);
            */
            modelBuilder.Entity<Rezervasyon>()
                .HasOne(r => r.Servis)
                .WithMany()
                .HasForeignKey(r => r.ServisId);

            modelBuilder.Entity<Rezervasyon>()
                .HasOne(r => r.Profesyonel)
                .WithMany(p => p.Rezervasyonlar)
                .HasForeignKey(r => r.ProfesyonelId);

            /* Relaciones para Profesional
            modelBuilder.Entity<Profesional>()
                //.HasMany(p => p.Cupos)
                .WithOne(c => c.Profesional)
                .HasForeignKey(c => c.ProfesionalId);
            */
            modelBuilder.Entity<Profesyonel>()
                .HasMany(p => p.ProfesyonelServisler)
                .WithOne(ps => ps.Profesyonel)
                .HasForeignKey(ps => ps.ProfesyonelId);
        }
    }
}
