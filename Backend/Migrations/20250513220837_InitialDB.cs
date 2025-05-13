using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
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
                    TransactionsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    GameHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    BetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsWin = table.Column<bool>(type: "bit", nullable: false),
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
                    { 1, 100m, new DateTime(1990, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "NiklasErEnMaskine@mail.com", null, "Niklas", "Maskine", true, "$2a$11$Xb1rp6D4YrQA/Tw3GUrGPu8FtL/e7jhcevNjlrT0LIz/lxXHsBTJ2", 12345678, 50m, 1 },
                    { 2, 75m, new DateTime(1990, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "johndoe@example.com", null, "John", "Doe", false, "$2a$11$c9yxY/YxL8itURbFAQCT2.lqqfNMP8R32y4qplxgY1dPnNFGzwXGi", 87654321, 33m, 0 },
                    { 3, 100m, new DateTime(1995, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "anna.jensen@example.com", null, "Anna", "Jensen", true, "$2a$11$u4YLqP0tM7Z7MYFJXCk/zuBoa/CxMEG8pNbHYeKkoKqq3xpwSReSW", 11111111, 20m, 0 },
                    { 4, 150m, new DateTime(1988, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "mark.larsen@example.com", null, "Mark", "Larsen", true, "$2a$11$cxs3XaweKBbbUOIjLJ59/OpyAXulKWGamxCOfFYVzzHmCmDh6QBVe", 22222222, 40m, 0 },
                    { 5, 200m, new DateTime(1992, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara.hansen@example.com", null, "Sara", "Hansen", false, "$2a$11$Ot31/jFQ6DWWAcvn23L2GuwacPDZXP7qSFMLquxpcDgB7NKx.8sUS", 33333333, 30m, 0 },
                    { 6, 300m, new DateTime(1985, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "peter.madsen@example.com", null, "Peter", "Madsen", true, "$2a$11$1pe1UZ6xvrcqucK/dGgJoOJ/EP6alBaPyEbeWsmpNTcLvlVOjf6Pm", 44444444, 70m, 0 },
                    { 7, 120m, new DateTime(1998, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "laura.poulsen@example.com", null, "Laura", "Poulsen", true, "$2a$11$WirgccVBkc4VZvO.SbWcU.oKQCWcoM3D/RSbsKNvF2iFGHJ6PphCu", 55555555, 25m, 0 },
                    { 8, 180m, new DateTime(1982, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "thomas.christensen@example.com", null, "Thomas", "Christensen", true, "$2a$11$/nA/zGDmy0O7pKUjE/eqBOIMvQrUF1Hz0y2c3sMklcVs6m7XhL61S", 66666666, 60m, 0 },
                    { 9, 220m, new DateTime(1994, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "emma.andersen@example.com", null, "Emma", "Andersen", true, "$2a$11$ZcM50yXmOZhCyuvEcKGID.pTJLz79bimZI0BQbW1nCDjl1O7HaOvK", 77777777, 80m, 0 },
                    { 10, 90m, new DateTime(1989, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "mikkel.olsen@example.com", null, "Mikkel", "Olsen", false, "$2a$11$FEqFXJ0zzGWRCck37wbHle0AAxXvSgj13OciYXHzqz4CoRMyqmTMK", 88888888, 10m, 0 }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "CategoryId", "ImageUrl", "JackpotAmount", "Name", "Status", "WebUrl" },
                values: new object[,]
                {
                    { 1, 1, "https://i.imgflip.com/7nz6q8.png?a484848", 10000m, "Football Match", true, null },
                    { 2, 2, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTUKs7TFCPgIxI0i4E3IwOiAEAGbdfCg8zKmA&s", 5000m, "Blackjack", true, null }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionsId", "Amount", "Date", "Direction", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, 500, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "In", "Deposit", 1 },
                    { 2, 300, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Out", "Withdrawal", 2 }
                });

            migrationBuilder.InsertData(
                table: "GameHistories",
                columns: new[] { "GameHistoryId", "BetAmount", "Date", "GameId", "IsJackpotWin", "IsWin", "UserId" },
                values: new object[,]
                {
                    { 1, 100m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, true, 1 },
                    { 2, 50m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, false, false, 2 }
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
