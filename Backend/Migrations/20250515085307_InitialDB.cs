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
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
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
                    { 1, 100m, new DateTime(1990, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "NiklasErEnMaskine@mail.com", null, "Niklas", "Maskine", true, "$2a$11$RrfyVsnVyjmeCf8owGeFBuA1naAgKKh4VVchCf5y.K73JgFi0MCXa", 12345678, 50m, 1 },
                    { 2, 75m, new DateTime(1990, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "johndoe@example.com", null, "John", "Doe", false, "$2a$11$scdPPMEeeAMtOLnnVMNicejgEaxoq4kHTWUJnxB8cwCUc2rTbhRoG", 87654321, 33m, 0 },
                    { 3, 100m, new DateTime(1995, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "anna.jensen@example.com", null, "Anna", "Jensen", true, "$2a$11$hNsXYSxRkYM..M7iBD0DfuqKH4oQiMI26IVhDr9fYAN5hjpDl6so.", 11111111, 20m, 0 },
                    { 4, 150m, new DateTime(1988, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "mark.larsen@example.com", null, "Mark", "Larsen", true, "$2a$11$m8CCZ838sViMxAUniptpP.S6l8B7ryUe9mnNPmqPrTyIqRJbYobsq", 22222222, 40m, 0 },
                    { 5, 200m, new DateTime(1992, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara.hansen@example.com", null, "Sara", "Hansen", false, "$2a$11$zBDgF0kMI15h0156EefKQeZ42pKttp.oQjM4PDD53iKajaWcBFJvq", 33333333, 30m, 0 },
                    { 6, 300m, new DateTime(1985, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "peter.madsen@example.com", null, "Peter", "Madsen", true, "$2a$11$m5x2e1z.851rMFwR4/fvYudm6z1CSEqyDqHcL1Al2OOouuHHzp22a", 44444444, 70m, 0 },
                    { 7, 120m, new DateTime(1998, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "laura.poulsen@example.com", null, "Laura", "Poulsen", true, "$2a$11$nwGSrv.ZJfb6PeMKU8X8UOgxSDP9gHKf5ITVtMwwVllzY.2019hBW", 55555555, 25m, 0 },
                    { 8, 180m, new DateTime(1982, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "thomas.christensen@example.com", null, "Thomas", "Christensen", true, "$2a$11$p4rQZwAC2rT7/4YK7t1xweR3CS0MJS/bSQqJw//H0dYXq2wz.Hcv.", 66666666, 60m, 0 },
                    { 9, 220m, new DateTime(1994, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "emma.andersen@example.com", null, "Emma", "Andersen", true, "$2a$11$iouAvbh7RBbR7rwcCXV0A.ZWnBg.ORZsiHt7c33I.ZnFqamUwSqIy", 77777777, 80m, 0 },
                    { 10, 90m, new DateTime(1989, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "mikkel.olsen@example.com", null, "Mikkel", "Olsen", false, "$2a$11$2muF5DXsptL7Iu9LiHabwef0/An/DRkv3j8SHGRYzsxacrfY5ko5S", 88888888, 10m, 0 }
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
                columns: new[] { "TransactionId", "Amount", "Date", "Direction", "Type", "UserId" },
                values: new object[,]
                {
                    { new Guid("58ed2209-1031-4aae-ada2-25c30d094e3e"), 300, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Out", "Withdrawal", 2 },
                    { new Guid("e633333d-98b1-4e0e-bfa3-cd03b1fb2895"), 500, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "In", "Deposit", 1 }
                });

            migrationBuilder.InsertData(
                table: "GameHistories",
                columns: new[] { "GameHistoryId", "BetAmount", "Date", "GameId", "IsJackpotWin", "IsWin", "UserId", "WasCashedOut", "WinAmount" },
                values: new object[,]
                {
                    { new Guid("5aa540fb-e97a-4f8b-bb10-fe75067b9af8"), 50m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, false, false, 2, false, 0m },
                    { new Guid("6fb00f2a-c831-4c2a-b2aa-a9a8cb8b32d0"), 100m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, true, 1, false, 0m }
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
