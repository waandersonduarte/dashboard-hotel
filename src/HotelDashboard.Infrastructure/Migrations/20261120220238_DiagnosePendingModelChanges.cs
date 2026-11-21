using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DiagnosePendingModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_charge_consumption_id_account",
                table: "charge");

            migrationBuilder.CreateIndex(
                name: "IX_charge_id_consumption",
                table: "charge",
                column: "id_consumption");

            migrationBuilder.AddForeignKey(
                name: "FK_charge_consumption_id_consumption",
                table: "charge",
                column: "id_consumption",
                principalTable: "consumption",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_charge_consumption_id_consumption",
                table: "charge");

            migrationBuilder.DropIndex(
                name: "IX_charge_id_consumption",
                table: "charge");

            migrationBuilder.AddForeignKey(
                name: "FK_charge_consumption_id_account",
                table: "charge",
                column: "id_account",
                principalTable: "consumption",
                principalColumn: "id");
        }
    }
}
