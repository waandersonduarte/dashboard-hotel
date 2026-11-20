using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCheckInDatesAndAddAccountConsumptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consumption_account_Id_account",
                table: "consumption");

            migrationBuilder.DropColumn(
                name: "departure_date",
                table: "checkin");

            migrationBuilder.DropColumn(
                name: "entry_date",
                table: "checkin");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "consumption",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "checkin_date",
                table: "checkin",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "checkout_date",
                table: "checkin",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_consumption_AccountId",
                table: "consumption",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_consumption_account_AccountId",
                table: "consumption",
                column: "AccountId",
                principalTable: "account",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_consumption_account_Id_account",
                table: "consumption",
                column: "Id_account",
                principalTable: "account",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consumption_account_AccountId",
                table: "consumption");

            migrationBuilder.DropForeignKey(
                name: "FK_consumption_account_Id_account",
                table: "consumption");

            migrationBuilder.DropIndex(
                name: "IX_consumption_AccountId",
                table: "consumption");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "consumption");

            migrationBuilder.DropColumn(
                name: "checkin_date",
                table: "checkin");

            migrationBuilder.DropColumn(
                name: "checkout_date",
                table: "checkin");

            migrationBuilder.AddColumn<DateTime>(
                name: "departure_date",
                table: "checkin",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "entry_date",
                table: "checkin",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_consumption_account_Id_account",
                table: "consumption",
                column: "Id_account",
                principalTable: "account",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
