using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AiDemoTransport.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTransportModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Transports_LicensePlate",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "LicensePlate",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Transports");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Transports",
                newName: "PickupLocationDetails");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Transports",
                newName: "PurchaseId");

            migrationBuilder.AddColumn<int>(
                name: "CarrierId",
                table: "Transports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryLocationDetails",
                table: "Transports",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduleDate",
                table: "Transports",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Transports",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Scheduled");

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CarrierId", "DeliveryLocationDetails", "PickupLocationDetails", "PurchaseId", "ScheduleDate", "Status" },
                values: new object[] { 101, "456 Business Ave, Corporate District, City B - Contact: Jane Smith, Phone: +1-555-0456", "123 Main St, Downtown, City A - Contact: John Doe, Phone: +1-555-0123", 1001, new DateTime(2026, 1, 3, 12, 25, 16, 50, DateTimeKind.Utc).AddTicks(5271), "Scheduled" });

            migrationBuilder.CreateIndex(
                name: "IX_Transports_CarrierId",
                table: "Transports",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_PurchaseId",
                table: "Transports",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_ScheduleDate",
                table: "Transports",
                column: "ScheduleDate");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_Status",
                table: "Transports",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transports_CarrierId",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Transports_PurchaseId",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Transports_ScheduleDate",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Transports_Status",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "DeliveryLocationDetails",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "ScheduleDate",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transports");

            migrationBuilder.RenameColumn(
                name: "PurchaseId",
                table: "Transports",
                newName: "Capacity");

            migrationBuilder.RenameColumn(
                name: "PickupLocationDetails",
                table: "Transports",
                newName: "Description");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Transports",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Transports",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LicensePlate",
                table: "Transports",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Transports",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Transports",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Transports",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransportId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Distance = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    EndLocation = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    EstimatedDuration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StartLocation = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_Transports_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RouteId = table.Column<int>(type: "integer", nullable: false),
                    TransportId = table.Column<int>(type: "integer", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AvailableSeats = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DepartureTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Scheduled"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Transports_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "Id", "CreatedAt", "Description", "Distance", "EndLocation", "EstimatedDuration", "IsActive", "Name", "StartLocation", "TransportId", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2026, 1, 2, 11, 35, 56, 373, DateTimeKind.Utc).AddTicks(3898), "Main route connecting central station to business district", 15.5m, "Business District", new TimeSpan(0, 0, 35, 0, 0), true, "Downtown Circuit", "Central Station", 1, null });

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Capacity", "CreatedAt", "Description", "IsActive", "LicensePlate", "Name", "Type", "UpdatedAt" },
                values: new object[] { 45, new DateTime(2026, 1, 2, 11, 35, 56, 373, DateTimeKind.Utc).AddTicks(3489), "Main city bus line serving downtown area", true, "CB-001-A", "City Bus Line A", "Bus", null });

            migrationBuilder.CreateIndex(
                name: "IX_Transports_LicensePlate",
                table: "Transports",
                column: "LicensePlate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Routes_TransportId",
                table: "Routes",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RouteId",
                table: "Schedules",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TransportId",
                table: "Schedules",
                column: "TransportId");
        }
    }
}
