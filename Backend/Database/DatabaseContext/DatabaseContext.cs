using Backend.Database.Entities;
using Backend.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static Backend.Helper.TransactionsHelper;

namespace Backend.Database.DatabaseContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DatabaseContext() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Categori> Categories { get; set; }
        public DbSet<GamesHistory> GamesHistories { get; set; }
        public DbSet<Transactions> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
                d => d.ToDateTime(TimeOnly.MinValue),
                d => DateOnly.FromDateTime(d)
            );

            ConfigureEntities(modelBuilder, dateOnlyConverter);
            SeedData(modelBuilder);
        }

        private void ConfigureEntities(ModelBuilder modelBuilder, ValueConverter<DateOnly, DateTime> dateOnlyConverter)
        {

            modelBuilder.Entity<User>()
                .Property(u => u.BirthDate)
                .HasConversion(dateOnlyConverter);

            modelBuilder.Entity<GamesHistory>()
                .Property(g => g.Date)
                .HasConversion(dateOnlyConverter);

            modelBuilder.Entity<Transactions>()
                .Property(t => t.Date)
                .HasConversion(dateOnlyConverter);

            // Relations opsætning
            modelBuilder.Entity<User>()
                .HasMany(u => u.GamesHistories)
                .WithOne(gh => gh.User)
                .HasForeignKey(gh => gh.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Transactions)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Game>()
                .HasMany(g => g.GamesHistories)
                .WithOne(gh => gh.Games)
                .HasForeignKey(gh => gh.GamesId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Categori>()
                .HasMany(c => c.Games)
                .WithOne(g => g.Category)
                .HasForeignKey(g => g.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Niklas",
                    LastName = "Maskine",
                    Role = Role.Admin,
                    Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"),
                    Email = "NiklasErEnMaskine@mail.com",
                    BirthDate = new DateOnly(1990, 5, 21),
                    PhoneNumber = 12345678,
                    NewsLetterIsSubscribed = true,
                    Balance = 100,
                    Profit = 50,
                    Loss = 25,
                    ExcludedUntil = null
                },
                new User
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    Role = Role.Customer,
                    Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"),
                    Email = "johndoe@example.com",
                    BirthDate = new DateOnly(1990, 5, 21),
                    PhoneNumber = 87654321,
                    NewsLetterIsSubscribed = false,
                    Balance = 75,
                    Profit = 33,
                    Loss = 55,
                    ExcludedUntil = null
                },
                    new User { Id = 3, FirstName = "Anna", LastName = "Jensen", Email = "anna.jensen@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), PhoneNumber = 11111111, BirthDate = new DateOnly(1995, 3, 10), NewsLetterIsSubscribed = true, Role = Role.Customer, Balance = 100, Profit = 20, Loss = 15, ExcludedUntil = null },
                    new User { Id = 4, FirstName = "Mark", LastName = "Larsen", Email = "mark.larsen@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), PhoneNumber = 22222222, BirthDate = new DateOnly(1988, 7, 23), NewsLetterIsSubscribed = true, Role = Role.Customer, Balance = 150, Profit = 40, Loss = 10, ExcludedUntil = null },
                    new User { Id = 5, FirstName = "Sara", LastName = "Hansen", Email = "sara.hansen@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), PhoneNumber = 33333333, BirthDate = new DateOnly(1992, 9, 30), NewsLetterIsSubscribed = false, Role = Role.Customer, Balance = 200, Profit = 30, Loss = 25, ExcludedUntil = null },
                    new User { Id = 6, FirstName = "Peter", LastName = "Madsen", Email = "peter.madsen@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), PhoneNumber = 44444444, BirthDate = new DateOnly(1985, 1, 15), NewsLetterIsSubscribed = true, Role = Role.Customer, Balance = 300, Profit = 70, Loss = 40, ExcludedUntil = null },
                    new User { Id = 7, FirstName = "Laura", LastName = "Poulsen", Email = "laura.poulsen@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), PhoneNumber = 55555555, BirthDate = new DateOnly(1998, 12, 12), NewsLetterIsSubscribed = true, Role = Role.Customer, Balance = 120, Profit = 25, Loss = 5, ExcludedUntil = null },
                    new User { Id = 8, FirstName = "Thomas", LastName = "Christensen", Email = "thomas.christensen@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), PhoneNumber = 66666666, BirthDate = new DateOnly(1982, 4, 8), NewsLetterIsSubscribed = true, Role = Role.Customer, Balance = 180, Profit = 60, Loss = 20, ExcludedUntil = null },
                    new User { Id = 9, FirstName = "Emma", LastName = "Andersen", Email = "emma.andersen@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), PhoneNumber = 77777777, BirthDate = new DateOnly(1994, 6, 5), NewsLetterIsSubscribed = true, Role = Role.Customer, Balance = 220, Profit = 80, Loss = 10, ExcludedUntil = null },
                    new User { Id = 10, FirstName = "Mikkel", LastName = "Olsen", Email = "mikkel.olsen@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), PhoneNumber = 88888888, BirthDate = new DateOnly(1989, 11, 19), NewsLetterIsSubscribed = false, Role = Role.Customer, Balance = 90, Profit = 10, Loss = 10, ExcludedUntil = null }

            );

            modelBuilder.Entity<Categori>().HasData(
                new Categori { Id = 1, Name = "Sports" },
                new Categori { Id = 2, Name = "Casino" }
            );

            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    Id = 1,
                    Name = "Football Match",
                    CategoryId = 1,
                    JackpotAmount = 10000,
                    ImageUrl = "https://i.imgflip.com/7nz6q8.png?a484848",
                    Status = true
                },
                new Game
                {
                    Id = 2,
                    Name = "Blackjack",
                    CategoryId = 2,
                    JackpotAmount = 5000,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTUKs7TFCPgIxI0i4E3IwOiAEAGbdfCg8zKmA&s",
                    Status = true
                },
                new Game
                {
                    Id = 3,
                    Name = "Bombastic",
                    CategoryId = 2,
                    JackpotAmount = 15000,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/70/Fireball.svg/1024px-Fireball.svg.png",
                    Status = true,
                    WebUrl = "bombastic"
                }

            );

            modelBuilder.Entity<GamesHistory>().HasData(
                new GamesHistory
                {
                    Id = 1,
                    UserId = 1,
                    GamesId = 1,
                    Date = new DateOnly(2024, 4, 1),
                    BetAmount = 100,
                    Win = true,
                    JackpotWin = false
                },
                new GamesHistory
                {
                    Id = 2,
                    UserId = 2,
                    GamesId = 2,
                    Date = new DateOnly(2024, 4, 2),
                    BetAmount = 50,
                    Win = false,
                    JackpotWin = false
                }
            );

            modelBuilder.Entity<Transactions>().HasData(
                new Transactions
                {
                    Id = 1,
                    UserId = 1,
                    Amount = 500,
                    Date = new DateOnly(2024, 4, 5),
                    Type = TransactionTypes.Deposit,
                    Direction = Directions.In
                },
                new Transactions
                {
                    Id = 2,
                    UserId = 2,
                    Amount = 300,
                    Date = new DateOnly(2024, 4, 6),
                    Type = TransactionTypes.Withdrawal,
                    Direction = Directions.Out
                }
            );
        }
    }
}
