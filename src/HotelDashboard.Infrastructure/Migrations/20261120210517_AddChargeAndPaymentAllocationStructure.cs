using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChargeAndPaymentAllocationStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_amount",
                table: "reservation");

            migrationBuilder.DropColumn(
                name: "total_amount",
                table: "account");

            migrationBuilder.CreateTable(
                name: "charge",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_reservation = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    id_account = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    id_consumption = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    type = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_charge", x => x.id);
                    table.ForeignKey(
                        name: "FK_charge_account_id_account",
                        column: x => x.id_account,
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_charge_consumption_ConsumptionId",
                        column: x => x.ConsumptionId,
                        principalTable: "consumption",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_charge_reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "reservation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payment_allocation",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_payment = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_charge = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChargeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_allocation", x => x.id);
                    table.ForeignKey(
                        name: "FK_payment_allocation_charge_ChargeId",
                        column: x => x.ChargeId,
                        principalTable: "charge",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_payment_allocation_charge_id_charge",
                        column: x => x.id_charge,
                        principalTable: "charge",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_payment_allocation_payment_id_payment",
                        column: x => x.id_payment,
                        principalTable: "payment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_charge_ConsumptionId",
                table: "charge",
                column: "ConsumptionId");

            migrationBuilder.CreateIndex(
                name: "IX_charge_id_account",
                table: "charge",
                column: "id_account");

            migrationBuilder.CreateIndex(
                name: "IX_charge_ReservationId",
                table: "charge",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_allocation_ChargeId",
                table: "payment_allocation",
                column: "ChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_allocation_id_charge",
                table: "payment_allocation",
                column: "id_charge");

            migrationBuilder.CreateIndex(
                name: "IX_payment_allocation_id_payment",
                table: "payment_allocation",
                column: "id_payment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment_allocation");

            migrationBuilder.DropTable(
                name: "charge");

            migrationBuilder.AddColumn<decimal>(
                name: "total_amount",
                table: "reservation",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "total_amount",
                table: "account",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
