using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class carte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId1",
                table: "Factures",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Factures_OrderId1",
                table: "Factures",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Factures_Orders_OrderId1",
                table: "Factures",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Orders_OrderId1",
                table: "Factures");

            migrationBuilder.DropIndex(
                name: "IX_Factures_OrderId1",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "Factures");
        }
    }
}
