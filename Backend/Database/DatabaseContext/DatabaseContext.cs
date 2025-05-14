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
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameHistory> GameHistories { get; set; }
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

            // Relations opsætning
            modelBuilder.Entity<User>()
                .HasMany(u => u.GameHistories)
                .WithOne(gh => gh.User)
                .HasForeignKey(gh => gh.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Transactions)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Game>()
                .HasMany(g => g.GameHistories)
                .WithOne(gh => gh.Game)
                .HasForeignKey(gh => gh.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
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
                    ExcludedUntil = null
                },
                    new User { Id = 3, FirstName = "Anna", LastName = "Jensen", Email = "anna.jensen@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), PhoneNumber = 11111111, BirthDate = new DateOnly(1995, 3, 10), NewsLetterIsSubscribed = true, Role = Role.Customer, Balance = 100, Profit = 20, ExcludedUntil = null },
                    new User { Id = 4, FirstName = "Mark", LastName = "Larsen", Email = "mark.larsen@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), PhoneNumber = 22222222, BirthDate = new DateOnly(1988, 7, 23), NewsLetterIsSubscribed = true, Role = Role.Customer, Balance = 150, Profit = 40, ExcludedUntil = null },
                    new User { Id = 5, FirstName = "Sara", LastName = "Hansen", Email = "sara.hansen@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), PhoneNumber = 33333333, BirthDate = new DateOnly(1992, 9, 30), NewsLetterIsSubscribed = false, Role = Role.Customer, Balance = 200, Profit = 30, ExcludedUntil = null },
                    new User { Id = 6, FirstName = "Peter", LastName = "Madsen", Email = "peter.madsen@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), PhoneNumber = 44444444, BirthDate = new DateOnly(1985, 1, 15), NewsLetterIsSubscribed = true, Role = Role.Customer, Balance = 300, Profit = 70, ExcludedUntil = null },
                    new User { Id = 7, FirstName = "Laura", LastName = "Poulsen", Email = "laura.poulsen@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), PhoneNumber = 55555555, BirthDate = new DateOnly(1998, 12, 12), NewsLetterIsSubscribed = true, Role = Role.Customer, Balance = 120, Profit = 25, ExcludedUntil = null },
                    new User { Id = 8, FirstName = "Thomas", LastName = "Christensen", Email = "thomas.christensen@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), PhoneNumber = 66666666, BirthDate = new DateOnly(1982, 4, 8), NewsLetterIsSubscribed = true, Role = Role.Customer, Balance = 180, Profit = 60, ExcludedUntil = null },
                    new User { Id = 9, FirstName = "Emma", LastName = "Andersen", Email = "emma.andersen@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), PhoneNumber = 77777777, BirthDate = new DateOnly(1994, 6, 5), NewsLetterIsSubscribed = true, Role = Role.Customer, Balance = 220, Profit = 80, ExcludedUntil = null },
                    new User { Id = 10, FirstName = "Mikkel", LastName = "Olsen", Email = "mikkel.olsen@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"), PhoneNumber = 88888888, BirthDate = new DateOnly(1989, 11, 19), NewsLetterIsSubscribed = false, Role = Role.Customer, Balance = 90, Profit = 10, ExcludedUntil = null }

            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Sports" },
                new Category { CategoryId = 2, Name = "Casino" }
            );

            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    GameId = 1,
                    Name = "Football Match",
                    CategoryId = 1,
                    JackpotAmount = 10000,
                    ImageUrl = "https://i.imgflip.com/7nz6q8.png?a484848",
                    Status = true
                },
                new Game
                {
                    GameId = 2,
                    Name = "Blackjack",
                    CategoryId = 2,
                    JackpotAmount = 5000,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTUKs7TFCPgIxI0i4E3IwOiAEAGbdfCg8zKmA&s",
                    Status = true
                }
            );

            modelBuilder.Entity<GameHistory>().HasData(
                new GameHistory
                {
                    GameHistoryId = Guid.NewGuid(),
                    UserId = 1,
                    GameId = 1,
                    Date = new DateTime(),
                    BetAmount = 100,
                    IsWin = true,
                    IsJackpotWin = false
                },
                new GameHistory
                {
                    GameHistoryId = Guid.NewGuid(),
                    UserId = 2,
                    GameId = 2,
                    Date = new DateTime(),
                    BetAmount = 50,
                    IsWin = false,
                    IsJackpotWin = false
                }
            );

            modelBuilder.Entity<Transactions>().HasData(
                new Transactions
                {
                    TransactionsId = Guid.NewGuid(),
                    UserId = 1,
                    Amount = 500,
                    Date = new DateTime(),
                    Type = TransactionTypes.Deposit,
                    Direction = Directions.In
                },
                new Transactions
                {
                    TransactionsId = Guid.NewGuid(),
                    UserId = 2,
                    Amount = 300,
                    Date = new DateTime(),
                    Type = TransactionTypes.Withdrawal,
                    Direction = Directions.Out
                }
            );
        }
    }
}
