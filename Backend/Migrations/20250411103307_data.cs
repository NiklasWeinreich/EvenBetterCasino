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
                    Balance = table.Column<int>(type: "int", nullable: false),
                    ExcludedUntil = table.Column<DateTime>(type: "datetime", nullable: true),
                    Profit = table.Column<int>(type: "int", nullable: false),
                    Loss = table.Column<int>(type: "int", nullable: false),
                    NewsLetterIsSubscribed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "BirthDate", "Email", "ExcludedUntil", "FirstName", "LastName", "Loss", "NewsLetterIsSubscribed", "Password", "PhoneNumber", "Profit" },
                values: new object[,]
                {
                    { 1, 100, new DateTime(1990, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "NiklasErEnMaskine@mail.com", null, "Niklas", "Maskine", 25, true, "$2a$11$jRwd7Ccf2OG9IkGKlgkVgekSBu1znypWT6FkQJaigJVDBg7YHtYny", 12345678, 50 },
                    { 2, 100, new DateTime(1990, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "johndoe@example.com", null, "John", "Doe", 25, true, "$2a$11$7SqqU/etxWtTI9HIF/f14O6pL81GYyFfU4CNcHbDBsVulWUhhD3MC", 12345678, 50 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
