using Microsoft.EntityFrameworkCore;
using TelemetriaMonitoramento.API.Models;

namespace TelemetriaMonitoramento.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Machine> Machines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Name).IsRequired().HasMaxLength(100);
                entity.Property(m => m.Location).IsRequired().HasMaxLength(100);
                entity.Property(m => m.Status).IsRequired();
            });
        }
    }
}
