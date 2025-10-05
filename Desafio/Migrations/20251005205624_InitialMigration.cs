using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Desafio.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<decimal>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Penalties",
                columns: table => new
                {
                    PenaltieId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DaysLateFrom = table.Column<int>(nullable: false),
                    DaysLateTo = table.Column<int>(nullable: false),
                    FinePercent = table.Column<decimal>(nullable: false),
                    DailyInterest = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalties", x => x.PenaltieId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Penalties");
        }
    }
}
