using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "checkin",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_guest = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_room = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    entry_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    departure_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkin", x => x.id);
                    table.ForeignKey(
                        name: "FK_checkin_guest_id_guest",
                        column: x => x.id_guest,
                        principalTable: "guest",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_checkin_room_id_room",
                        column: x => x.id_room,
                        principalTable: "room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_checkin_id_guest",
                table: "checkin",
                column: "id_guest");

            migrationBuilder.CreateIndex(
                name: "IX_checkin_id_room",
                table: "checkin",
                column: "id_room");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checkin");
        }
    }
}
