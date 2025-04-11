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

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                FirstName = "Niklas",
                LastName = "Maskine",
                Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"),
                Email = "NiklasErEnMaskine@mail.com",
                BirthDate = new DateOnly(1990, 5, 21),
                PhoneNumber = 12345678,
                NewsLetterIsSubscribed = true,
                Balance = 100,
                Profit = 50,
                Loss = 25,
                ExcludedUntil = null

            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 2,
                FirstName = "John",
                LastName = "Doe",
                Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"),
                Email = "johndoe@example.com",
                BirthDate = new DateOnly(1990, 5, 21),
                PhoneNumber = 12345678,
                NewsLetterIsSubscribed = true,
                Balance = 100,
                Profit = 50,
                Loss = 25,
                ExcludedUntil = null

            });

        }
    }
}
