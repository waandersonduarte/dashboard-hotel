using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DropColumnAccountId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consumption_account_AccountId",
                table: "consumption");

            migrationBuilder.DropIndex(
                name: "IX_consumption_AccountId",
                table: "consumption");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "consumption");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
