using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAccountConsumptionRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consumption_account_Id_account",
                table: "consumption");

            migrationBuilder.RenameColumn(
                name: "Id_account",
                table: "consumption",
                newName: "id_account");

            migrationBuilder.RenameIndex(
                name: "IX_consumption_Id_account",
                table: "consumption",
                newName: "IX_consumption_id_account");

            migrationBuilder.AddForeignKey(
                name: "FK_consumption_account_id_account",
                table: "consumption",
                column: "id_account",
                principalTable: "account",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consumption_account_id_account",
                table: "consumption");

            migrationBuilder.RenameColumn(
                name: "id_account",
                table: "consumption",
                newName: "Id_account");

            migrationBuilder.RenameIndex(
                name: "IX_consumption_id_account",
                table: "consumption",
                newName: "IX_consumption_Id_account");

            migrationBuilder.AddForeignKey(
                name: "FK_consumption_account_Id_account",
                table: "consumption",
                column: "Id_account",
                principalTable: "account",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
