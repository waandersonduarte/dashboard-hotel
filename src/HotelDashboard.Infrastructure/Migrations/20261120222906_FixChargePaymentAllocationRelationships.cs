using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixChargePaymentAllocationRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payment_allocation_charge_ChargeId",
                table: "payment_allocation");

            migrationBuilder.DropIndex(
                name: "IX_payment_allocation_ChargeId",
                table: "payment_allocation");

            migrationBuilder.DropColumn(
                name: "ChargeId",
                table: "payment_allocation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChargeId",
                table: "payment_allocation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_payment_allocation_ChargeId",
                table: "payment_allocation",
                column: "ChargeId");

            migrationBuilder.AddForeignKey(
                name: "FK_payment_allocation_charge_ChargeId",
                table: "payment_allocation",
                column: "ChargeId",
                principalTable: "charge",
                principalColumn: "id");
        }
    }
}
