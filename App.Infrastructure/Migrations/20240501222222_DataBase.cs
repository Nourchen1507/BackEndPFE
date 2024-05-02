using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MontantTTC",
                table: "Factures",
                newName: "SoldeAvant");

            migrationBuilder.RenameColumn(
                name: "MontantHT",
                table: "Factures",
                newName: "SoldeApres");

            migrationBuilder.AddColumn<string>(
                name: "CIN",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SoldeCarte",
                columns: table => new
                {
                    CIN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SoldeDisponible = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldeCarte", x => x.CIN);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Montant = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoldeAvant = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoldeApres = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumCommande = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTransaction = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoldeCarte");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropColumn(
                name: "CIN",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "SoldeAvant",
                table: "Factures",
                newName: "MontantTTC");

            migrationBuilder.RenameColumn(
                name: "SoldeApres",
                table: "Factures",
                newName: "MontantHT");
        }
    }
}
