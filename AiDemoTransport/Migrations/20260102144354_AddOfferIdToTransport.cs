using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiDemoTransport.Migrations
{
    /// <inheritdoc />
    public partial class AddOfferIdToTransport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "Transports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OfferId", "ScheduleDate" },
                values: new object[] { 0, new DateTime(2026, 1, 3, 14, 43, 54, 238, DateTimeKind.Utc).AddTicks(4712) });

            migrationBuilder.CreateIndex(
                name: "IX_Transports_OfferId",
                table: "Transports",
                column: "OfferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transports_OfferId",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Transports");

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 1,
                column: "ScheduleDate",
                value: new DateTime(2026, 1, 3, 12, 25, 16, 50, DateTimeKind.Utc).AddTicks(5271));
        }
    }
}
