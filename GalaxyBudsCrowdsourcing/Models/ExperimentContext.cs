using GalaxyBudsCrowdsourcing.Converters;
using Microsoft.EntityFrameworkCore;

namespace GalaxyBudsCrowdsourcing.Models
{
    public class ExperimentContext(DbContextOptions<ExperimentContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExperimentItem>()
                .ToTable("Experiments");

            modelBuilder.Entity<ExperimentItem>()
                .Property(c => c.TargetDevices)
                .HasConversion(new EnumArrayConverter<Devices>());
            
            modelBuilder.Entity<ExperimentItem>()
                .Property(c => c.Environment)
                .HasConversion(new EnumConverter<Environment>());
            
           
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ExperimentItem> ExperimentItems { get; init; }
    }
}