using Backend.Database.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend.Database.DatabaseContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
                d => d.ToDateTime(TimeOnly.MinValue),
                d => DateOnly.FromDateTime(d)
            );

            modelBuilder.Entity<User>()
                .Property(u => u.BirthDate)
                .HasConversion(dateOnlyConverter);
        }
    }
}
