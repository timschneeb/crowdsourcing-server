using GalaxyBudsCrowdsourcing.Converters;
using Microsoft.EntityFrameworkCore;

namespace GalaxyBudsCrowdsourcing.Models
{
    public class ResultContext(DbContextOptions<ResultContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResultItem>()
                .ToTable("Results");

            modelBuilder.Entity<ResultItem>()
                .Property(c => c.Device)
                .HasConversion(new EnumConverter<Devices>());
            
            modelBuilder.Entity<ResultItem>()
                .Property(c => c.Environment)
                .HasConversion(new EnumConverter<Environment>());
            
            modelBuilder.Entity<ResultItem>()
                .Property(c => c.Platform)
                .HasConversion(new EnumConverter<Platforms>());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ResultItem> ResultItems { get; init; }
    }
}