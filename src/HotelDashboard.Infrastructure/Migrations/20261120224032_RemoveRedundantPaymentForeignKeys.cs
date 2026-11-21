using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRedundantPaymentForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payment_account_id_account",
                table: "payment");

            migrationBuilder.DropForeignKey(
                name: "FK_payment_reservation_id_reservation",
                table: "payment");

            migrationBuilder.DropIndex(
                name: "IX_payment_id_account",
                table: "payment");

            migrationBuilder.DropIndex(
                name: "IX_payment_id_reservation",
                table: "payment");

            migrationBuilder.DropColumn(
                name: "id_account",
                table: "payment");

            migrationBuilder.DropColumn(
                name: "id_reservation",
                table: "payment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "id_account",
                table: "payment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "id_reservation",
                table: "payment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_payment_id_account",
                table: "payment",
                column: "id_account");

            migrationBuilder.CreateIndex(
                name: "IX_payment_id_reservation",
                table: "payment",
                column: "id_reservation");

            migrationBuilder.AddForeignKey(
                name: "FK_payment_account_id_account",
                table: "payment",
                column: "id_account",
                principalTable: "account",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_payment_reservation_id_reservation",
                table: "payment",
                column: "id_reservation",
                principalTable: "reservation",
                principalColumn: "id");
        }
    }
}
