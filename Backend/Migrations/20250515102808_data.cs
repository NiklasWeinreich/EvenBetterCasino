using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExcludedUntil = table.Column<DateTime>(type: "datetime", nullable: true),
                    Profit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    NewsLetterIsSubscribed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    WebUrl = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    JackpotAmount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Games_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionsId);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameHistories",
                columns: table => new
                {
                    GameHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    BetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WinAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsWin = table.Column<bool>(type: "bit", nullable: false),
                    WasCashedOut = table.Column<bool>(type: "bit", nullable: false),
                    IsJackpotWin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameHistories", x => x.GameHistoryId);
                    table.ForeignKey(
                        name: "FK_GameHistories_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Sports" },
                    { 2, "Casino" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "BirthDate", "Email", "ExcludedUntil", "FirstName", "LastName", "NewsLetterIsSubscribed", "Password", "PhoneNumber", "Profit", "Role" },
                values: new object[,]
                {
                    { 1, 100m, new DateTime(1990, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "NiklasErEnMaskine@mail.com", null, "Niklas", "Maskine", true, "$2a$11$c1fToxLG.wS0CISaUSaH2OYPbC2Xst7SfwNF5w9rI.HD1ZCjtkBAy", 12345678, 50m, 1 },
                    { 2, 75m, new DateTime(1990, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "johndoe@example.com", null, "John", "Doe", false, "$2a$11$ZI/xqPfj/qdmePBgLb5xNO4lZDlrN.7sNRmE1th0P5Px1hFyaHAh.", 87654321, 33m, 0 },
                    { 3, 100m, new DateTime(1995, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "anna.jensen@example.com", null, "Anna", "Jensen", true, "$2a$11$OPQY1FuVpNrb8UuJixT9Nu/hI6SOJARkR4S9UoBoOPzwLocq1qNyu", 11111111, 20m, 0 },
                    { 4, 150m, new DateTime(1988, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "mark.larsen@example.com", null, "Mark", "Larsen", true, "$2a$11$H6VUx0.d0WyLGrs8254dGu2y7h4K2YZaQyMIBnHXlkExxmW0REWZu", 22222222, 40m, 0 },
                    { 5, 200m, new DateTime(1992, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara.hansen@example.com", null, "Sara", "Hansen", false, "$2a$11$5dSCEUq5CFJ5ain.0QO6EeM03D9nz16mRWf1esqTpS73c2x6zVgCm", 33333333, 30m, 0 },
                    { 6, 300m, new DateTime(1985, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "peter.madsen@example.com", null, "Peter", "Madsen", true, "$2a$11$.cjADPF4CWQxrV7W8ne6.O568TGHe5lpEahVMfsEkMBA/L9U4b9Vi", 44444444, 70m, 0 },
                    { 7, 120m, new DateTime(1998, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "laura.poulsen@example.com", null, "Laura", "Poulsen", true, "$2a$11$t8EcLX4EWqmEZMlmW58byexgkmLsLutV8HydWxMjYK14LuOCxaV16", 55555555, 25m, 0 },
                    { 8, 180m, new DateTime(1982, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "thomas.christensen@example.com", null, "Thomas", "Christensen", true, "$2a$11$Imv2iC6Gf.c8o.6xV/RnnuSIN1xkhXr40Zh1.NwrEShmdiLJ3wHvK", 66666666, 60m, 0 },
                    { 9, 220m, new DateTime(1994, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "emma.andersen@example.com", null, "Emma", "Andersen", true, "$2a$11$Br3AFT.I.AcjW7z2WlWCp.9n6CL7W.ysN0NLSBauUBfJ68.dYH5sq", 77777777, 80m, 0 },
                    { 10, 90m, new DateTime(1989, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "mikkel.olsen@example.com", null, "Mikkel", "Olsen", false, "$2a$11$QjZo6/E3gzKcb5fwpFmSouoyGQ/kQHJWI/Eeez3gWao2l9bLsDpzG", 88888888, 10m, 0 }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "CategoryId", "ImageUrl", "JackpotAmount", "Name", "Status", "WebUrl", "description" },
                values: new object[,]
                {
                    { 1, 1, "https://assets.funnygames.dk/2/114572/100319/1024x1024/yatzy.webp", 10000m, "Yatzy", true, "yatzy", "Klassisk terningspil, hvor du skal slå forskellige kombinationer som \"fem ens\" og \"fuldt hus\".\r\nStrategi og held går hånd i hånd i jagten på den højeste score." },
                    { 2, 2, "https://cdn.prod.website-files.com/5ae2e7a18cb7532f0710bdfb/5e21d7084c5acfd2a75b5c0f_small.jpg", 5000m, "Dice", true, "dice", "Gæt om det næste terningekast bliver højere eller lavere end det forrige.\r\nEt hurtigt og simpelt spil, hvor intuition og held afgør sejren." },
                    { 3, 2, "https://mediumrare.imgix.net/12c3bb0487e2239772248e61550a121ee20fe8400a63f386d08896d1122d1655?q=85", 15000m, "Bombastic", true, "bombastic", "Har du nerver er stål? er du gjort af det rette stof? .\r\nHvor mange gange kan du klikke på bomben inden den springer! " },
                    { 4, 2, "https://play-lh.googleusercontent.com/N4wmqXoa1smDCr8Iuc8SdZXJL7N3HJQbbB2bHe-DM4HGjMXX6fHnkJ6htuQAiOQRvQIy", 0m, "KENO", true, "Keno", "Vælg dine lykketal og se, hvor mange du rammer, når tallene bliver trukket.\r\nEt simpelt og spændende lotterispil med chance for store gevinster." }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionsId", "Amount", "Date", "Direction", "Type", "UserId" },
                values: new object[,]
                {
                    { new Guid("32263e72-dcb6-4327-9afa-e0faccbc8583"), 500, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "In", "Deposit", 1 },
                    { new Guid("89d66529-5df1-4d8d-9e17-f918866093ef"), 300, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Out", "Withdrawal", 2 }
                });

            migrationBuilder.InsertData(
                table: "GameHistories",
                columns: new[] { "GameHistoryId", "BetAmount", "Date", "GameId", "IsJackpotWin", "IsWin", "UserId", "WasCashedOut", "WinAmount" },
                values: new object[,]
                {
                    { new Guid("18527b1c-0184-4e97-8c73-dd70ce0e2636"), 100m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, true, 1, false, 0m },
                    { new Guid("5c7c166c-e03a-4385-9eff-d5bb73a6ac40"), 50m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, false, false, 2, false, 0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameHistories_GameId",
                table: "GameHistories",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameHistories_UserId",
                table: "GameHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_CategoryId",
                table: "Games",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameHistories");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
