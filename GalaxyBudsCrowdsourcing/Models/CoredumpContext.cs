using GalaxyBudsCrowdsourcing.Converters;
using Microsoft.EntityFrameworkCore;

namespace GalaxyBudsCrowdsourcing.Models
{
    public class CoredumpContext(DbContextOptions<CoredumpContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoredumpItem>()
                .ToTable("Coredumps");

            modelBuilder.Entity<CoredumpItem>()
                .Property(c => c.Device)
                .HasConversion(new EnumConverter<Devices>());

            modelBuilder.Entity<CoredumpItem>()
                .Property(c => c.Platform)
                .HasConversion(new EnumConverter<Platforms>());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CoredumpItem> CoredumpItems { get; init; }
    }
}