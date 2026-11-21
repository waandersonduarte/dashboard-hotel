using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixChargeAccountRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_charge_account_AccountId",
                table: "charge");

            migrationBuilder.DropForeignKey(
                name: "FK_charge_account_id_account",
                table: "charge");

            migrationBuilder.DropIndex(
                name: "IX_charge_AccountId",
                table: "charge");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "charge");

            migrationBuilder.AddForeignKey(
                name: "FK_charge_account_id_account",
                table: "charge",
                column: "id_account",
                principalTable: "account",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_charge_account_id_account",
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

            migrationBuilder.AddForeignKey(
                name: "FK_charge_account_AccountId",
                table: "charge",
                column: "AccountId",
                principalTable: "account",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_charge_account_id_account",
                table: "charge",
                column: "id_account",
                principalTable: "account",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
