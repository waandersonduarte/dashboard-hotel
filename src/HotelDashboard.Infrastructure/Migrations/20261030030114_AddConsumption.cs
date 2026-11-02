using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConsumption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "consumption",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_account = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    item = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consumption", x => x.id);
                    table.ForeignKey(
                        name: "FK_consumption_account_Id_account",
                        column: x => x.Id_account,
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_consumption_Id_account",
                table: "consumption",
                column: "Id_account");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "consumption");
        }
    }
}
