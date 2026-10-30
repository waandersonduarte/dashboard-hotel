using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price_per_night",
                table: "room");

            migrationBuilder.AddColumn<decimal>(
                name: "daily_rate",
                table: "room",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "total_amount",
                table: "reservation",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_checkin = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    date_closing = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.id);
                    table.ForeignKey(
                        name: "FK_account_checkin_id_checkin",
                        column: x => x.id_checkin,
                        principalTable: "checkin",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_reservation = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    id_account = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    method_payment = table.Column<int>(type: "int", nullable: false),
                    payment_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_payment_account_id_account",
                        column: x => x.id_account,
                        principalTable: "account",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_payment_reservation_id_reservation",
                        column: x => x.id_reservation,
                        principalTable: "reservation",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_id_checkin",
                table: "account",
                column: "id_checkin");

            migrationBuilder.CreateIndex(
                name: "IX_payment_id_account",
                table: "payment",
                column: "id_account");

            migrationBuilder.CreateIndex(
                name: "IX_payment_id_reservation",
                table: "payment",
                column: "id_reservation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropColumn(
                name: "daily_rate",
                table: "room");

            migrationBuilder.DropColumn(
                name: "total_amount",
                table: "reservation");

            migrationBuilder.AddColumn<double>(
                name: "price_per_night",
                table: "room",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
