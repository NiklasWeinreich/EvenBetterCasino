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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
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
                    Balance = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    ExcludedUntil = table.Column<DateTime>(type: "datetime", nullable: true),
                    Profit = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Loss = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    NewsLetterIsSubscribed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    KategoriId = table.Column<int>(type: "int", nullable: false),
                    JackpotAmount = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Categories_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamesHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    BetAmount = table.Column<int>(type: "int", nullable: false),
                    Win = table.Column<bool>(type: "bit", nullable: false),
                    JackpotWin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamesHistories_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamesHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sports" },
                    { 2, "Casino" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "BirthDate", "Email", "ExcludedUntil", "FirstName", "LastName", "Loss", "NewsLetterIsSubscribed", "Password", "PhoneNumber", "Profit", "Role" },
                values: new object[,]
                {
                    { 1, 100m, new DateTime(1990, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "NiklasErEnMaskine@mail.com", null, "Niklas", "Maskine", 25, true, "$2a$11$LTHVHhoBQVjsN/HMJcxmgefHIX2EmT2Uju.A4zeqdVtCOB9UbfMHe", 12345678, 50m, 1 },
                    { 2, 75m, new DateTime(1990, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "johndoe@example.com", null, "John", "Doe", 55, false, "$2a$11$3kTnvLj1KgG6tVUBwfUrN.AefnS2s5mpxhQcQQqvkQbNfPvfniliy", 87654321, 33m, 0 },
                    { 3, 100m, new DateTime(1995, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "anna.jensen@example.com", null, "Anna", "Jensen", 15, true, "$2a$11$aADIm55dd7ec4jGQ0YpGwubDR69wkWQ0yQX1UsNRYt26O9llTym8m", 11111111, 20m, 0 },
                    { 4, 150m, new DateTime(1988, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "mark.larsen@example.com", null, "Mark", "Larsen", 10, true, "$2a$11$H0rTTQvDlSryyT77aEx4ou9PZhVZMj8aX6j/z/16VfhSAKlE5WO3.", 22222222, 40m, 0 },
                    { 5, 200m, new DateTime(1992, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara.hansen@example.com", null, "Sara", "Hansen", 25, false, "$2a$11$cV5Fm8SA8f8TSupv2R.eD.LME1Mq1KlWSB7qu9PCZFntAH65VDsxy", 33333333, 30m, 0 },
                    { 6, 300m, new DateTime(1985, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "peter.madsen@example.com", null, "Peter", "Madsen", 40, true, "$2a$11$7yoKBEC/int4z9nHhxfa/uZNnW2yqkp585HKeHASm/OpgEaNX78d.", 44444444, 70m, 0 },
                    { 7, 120m, new DateTime(1998, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "laura.poulsen@example.com", null, "Laura", "Poulsen", 5, true, "$2a$11$zN3KAbYDjY3UdWAB.iiquubPusocqE74xRogdjTME1YynW9YFDF.i", 55555555, 25m, 0 },
                    { 8, 180m, new DateTime(1982, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "thomas.christensen@example.com", null, "Thomas", "Christensen", 20, true, "$2a$11$xWYTwG6OmhU0itqAjn/JXuGSNqS4V3O/ZMGFenQFU4yV6hxM4l5ym", 66666666, 60m, 0 },
                    { 9, 220m, new DateTime(1994, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "emma.andersen@example.com", null, "Emma", "Andersen", 10, true, "$2a$11$yMVlTMFy0CZDF3GVs2n98ON3NParSEMglvUZjVFQEYNafYmOHX0yK", 77777777, 80m, 0 },
                    { 10, 90m, new DateTime(1989, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "mikkel.olsen@example.com", null, "Mikkel", "Olsen", 10, false, "$2a$11$el2LQZc0uVX1Yg6hLi4iC.W8.ZxsHw3xFhSsiUgzpmG/TF/orVedK", 88888888, 10m, 0 }
                });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "Id", "ImageUrl", "JackpotAmount", "KategoriId", "Name", "Status" },
                values: new object[,]
                {
                    { 1, "https://i.imgflip.com/7nz6q8.png?a484848", 10000, 1, "Football Match", true },
                    { 2, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTUKs7TFCPgIxI0i4E3IwOiAEAGbdfCg8zKmA&s", 5000, 2, "Blackjack", true }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "Date", "Direction", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, 500, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "In", "Deposit", 1 },
                    { 2, 300, new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Out", "Withdrawal", 2 }
                });

            migrationBuilder.InsertData(
                table: "GamesHistories",
                columns: new[] { "Id", "BetAmount", "Date", "GamesId", "JackpotWin", "UserId", "Win" },
                values: new object[,]
                {
                    { 1, 100, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, 1, true },
                    { 2, 50, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, false, 2, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_KategoriId",
                table: "Game",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesHistories_GamesId",
                table: "GamesHistories",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesHistories_UserId",
                table: "GamesHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamesHistories");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
