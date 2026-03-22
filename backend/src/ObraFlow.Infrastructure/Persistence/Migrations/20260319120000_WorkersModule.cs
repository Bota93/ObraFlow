using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObraFlow.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class WorkersModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "workers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "workers",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "workers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2026, 3, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AddColumn<decimal>(
                name: "HourlyRate",
                table: "workers",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "workers",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "workers",
                type: "character varying(140)",
                maxLength: 140,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(120)",
                oldMaxLength: 120);

            migrationBuilder.InsertData(
                table: "workers",
                columns: new[] { "Id", "CreatedAtUtc", "HourlyRate", "IsActive", "Name", "PhoneNumber", "Role" },
                values: new object[,]
                {
                    { new Guid("6d4c130b-d3ec-4da3-a7c3-f8ff5df74f01"), new DateTime(2026, 3, 1, 8, 0, 0, DateTimeKind.Utc), 34.50m, true, "Miguel Torres", "+34 600 111 222", "Site Supervisor" },
                    { new Guid("9a9d3e2e-bf2e-4aa7-a714-bdd4db98a102"), new DateTime(2026, 3, 2, 8, 0, 0, DateTimeKind.Utc), 28.75m, true, "Laura Martinez", "+34 600 333 444", "Heavy Equipment Operator" },
                    { new Guid("45df4f81-bd59-46b0-9df5-baa8543dca03"), new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc), 26.25m, true, "Carlos Ramirez", "+34 600 555 666", "Safety Technician" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "workers",
                keyColumn: "Id",
                keyValue: new Guid("45df4f81-bd59-46b0-9df5-baa8543dca03"));

            migrationBuilder.DeleteData(
                table: "workers",
                keyColumn: "Id",
                keyValue: new Guid("6d4c130b-d3ec-4da3-a7c3-f8ff5df74f01"));

            migrationBuilder.DeleteData(
                table: "workers",
                keyColumn: "Id",
                keyValue: new Guid("9a9d3e2e-bf2e-4aa7-a714-bdd4db98a102"));

            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "workers");

            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "workers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "workers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "workers",
                type: "character varying(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(140)",
                oldMaxLength: 140);

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "workers",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "workers",
                newName: "FullName");
        }
    }
}
