using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixChargeForeignKeyRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_charge_consumption_ConsumptionId",
                table: "charge");

            migrationBuilder.DropForeignKey(
                name: "FK_charge_reservation_ReservationId",
                table: "charge");

            migrationBuilder.DropIndex(
                name: "IX_charge_ConsumptionId",
                table: "charge");

            migrationBuilder.DropIndex(
                name: "IX_charge_ReservationId",
                table: "charge");

            migrationBuilder.DropColumn(
                name: "ConsumptionId",
                table: "charge");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "charge");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "charge",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_charge_AccountId",
                table: "charge",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_charge_id_reservation",
                table: "charge",
                column: "id_reservation");

            migrationBuilder.AddForeignKey(
                name: "FK_charge_account_AccountId",
                table: "charge",
                column: "AccountId",
                principalTable: "account",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_charge_consumption_id_account",
                table: "charge",
                column: "id_account",
                principalTable: "consumption",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_charge_reservation_id_reservation",
                table: "charge",
                column: "id_reservation",
                principalTable: "reservation",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_charge_account_AccountId",
                table: "charge");

            migrationBuilder.DropForeignKey(
                name: "FK_charge_consumption_id_account",
                table: "charge");

            migrationBuilder.DropForeignKey(
                name: "FK_charge_reservation_id_reservation",
                table: "charge");

            migrationBuilder.DropIndex(
                name: "IX_charge_AccountId",
                table: "charge");

            migrationBuilder.DropIndex(
                name: "IX_charge_id_reservation",
                table: "charge");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "charge");

            migrationBuilder.AddColumn<Guid>(
                name: "ConsumptionId",
                table: "charge",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "charge",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_charge_ConsumptionId",
                table: "charge",
                column: "ConsumptionId");

            migrationBuilder.CreateIndex(
                name: "IX_charge_ReservationId",
                table: "charge",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_charge_consumption_ConsumptionId",
                table: "charge",
                column: "ConsumptionId",
                principalTable: "consumption",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_charge_reservation_ReservationId",
                table: "charge",
                column: "ReservationId",
                principalTable: "reservation",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
