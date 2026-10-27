using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCheckin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_checkin_guest_id_guest",
                table: "checkin");

            migrationBuilder.DropForeignKey(
                name: "FK_checkin_room_id_room",
                table: "checkin");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_room",
                table: "checkin",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_guest",
                table: "checkin",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_checkin_guest_id_guest",
                table: "checkin",
                column: "id_guest",
                principalTable: "guest",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_checkin_room_id_room",
                table: "checkin",
                column: "id_room",
                principalTable: "room",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_checkin_guest_id_guest",
                table: "checkin");

            migrationBuilder.DropForeignKey(
                name: "FK_checkin_room_id_room",
                table: "checkin");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_room",
                table: "checkin",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_guest",
                table: "checkin",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_checkin_guest_id_guest",
                table: "checkin",
                column: "id_guest",
                principalTable: "guest",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_checkin_room_id_room",
                table: "checkin",
                column: "id_room",
                principalTable: "room",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
