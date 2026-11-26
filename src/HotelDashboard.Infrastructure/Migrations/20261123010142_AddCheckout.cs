using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "checkout",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_checkin = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    checkout_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkout", x => x.id);
                    table.ForeignKey(
                        name: "FK_checkout_checkin_id_checkin",
                        column: x => x.id_checkin,
                        principalTable: "checkin",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_checkout_id_checkin",
                table: "checkout",
                column: "id_checkin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checkout");
        }
    }
}
