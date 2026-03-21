using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObraFlow.Infrastructure.Persistence.Migrations;

public partial class DashboardSeedData : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "workers",
            columns: new[] { "Id", "CreatedAtUtc", "HourlyRate", "IsActive", "Name", "PhoneNumber", "Role" },
            values: new object[,]
            {
                { new Guid("2f99d8d5-2f8b-4d24-9781-1f1f7ad7d104"), new DateTime(2026, 3, 4, 8, 0, 0, DateTimeKind.Utc), 31.90m, true, "Sofia Navarro", "+34 600 777 888", "Project Engineer" },
                { new Guid("dbf81307-b91f-49f1-8ab5-46cd7b7ed105"), new DateTime(2026, 3, 5, 8, 0, 0, DateTimeKind.Utc), 29.80m, true, "Diego Perez", "+34 600 999 000", "Foreman" },
                { new Guid("f44da1bc-441f-4de3-b2f4-a9798db17a06"), new DateTime(2026, 3, 6, 8, 0, 0, DateTimeKind.Utc), 24.60m, true, "Elena Castro", "+34 611 111 222", "Logistics Coordinator" },
                { new Guid("8ab7e0ff-e769-490f-b503-96b7c6ef5d07"), new DateTime(2026, 3, 7, 8, 0, 0, DateTimeKind.Utc), 27.40m, false, "Javier Ortega", "+34 611 333 444", "Masonry Specialist" }
            });

        migrationBuilder.InsertData(
            table: "incidents",
            columns: new[] { "Id", "Description", "ReportedAtUtc", "Status", "Title" },
            values: new object[,]
            {
                { new Guid("778f5d0e-f8d8-4b70-a203-9161c8a89001"), "Water accumulation was detected near the service corridor after overnight rain.", new DateTime(2026, 3, 21, 7, 45, 0, DateTimeKind.Utc), 1, "Water ingress in basement corridor" },
                { new Guid("778f5d0e-f8d8-4b70-a203-9161c8a89002"), "The access gate on the west scaffold bay requires replacement before next shift.", new DateTime(2026, 3, 20, 9, 20, 0, DateTimeKind.Utc), 2, "Scaffold access gate damaged" },
                { new Guid("778f5d0e-f8d8-4b70-a203-9161c8a89003"), "Morning concrete pour was delayed due to a blocked supplier route and was rescheduled.", new DateTime(2026, 3, 19, 6, 50, 0, DateTimeKind.Utc), 3, "Concrete delivery delay" },
                { new Guid("778f5d0e-f8d8-4b70-a203-9161c8a89004"), "Several replacement helmets and gloves are pending for the finishing subcontractor.", new DateTime(2026, 3, 18, 12, 15, 0, DateTimeKind.Utc), 2, "PPE shortage in finishing crew" },
                { new Guid("778f5d0e-f8d8-4b70-a203-9161c8a89005"), "Temporary lighting failed in the protected storage area, reducing visibility for late shifts.", new DateTime(2026, 3, 17, 17, 30, 0, DateTimeKind.Utc), 1, "Lighting failure in storage area" },
                { new Guid("778f5d0e-f8d8-4b70-a203-9161c8a89006"), "A forklift touched the temporary barriers at low speed, with no injuries reported.", new DateTime(2026, 3, 16, 10, 5, 0, DateTimeKind.Utc), 3, "Minor forklift collision with barriers" }
            });

        migrationBuilder.InsertData(
            table: "dailyReports",
            columns: new[] { "Id", "Date", "Description", "HoursWorked", "WorkerId" },
            values: new object[,]
            {
                { new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68001"), new DateOnly(2026, 3, 21), "Coordinated subcontractors and reviewed progress in the structural zone.", 8.00m, new Guid("6d4c130b-d3ec-4da3-a7c3-f8ff5df74f01") },
                { new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68002"), new DateOnly(2026, 3, 21), "Excavation support and machinery checks completed near the north access.", 7.50m, new Guid("9a9d3e2e-bf2e-4aa7-a714-bdd4db98a102") },
                { new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68003"), new DateOnly(2026, 3, 20), "Performed safety inspection and updated risk signage after equipment movement.", 8.00m, new Guid("45df4f81-bd59-46b0-9df5-baa8543dca03") },
                { new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68004"), new DateOnly(2026, 3, 20), "Reviewed concrete pour schedule and approved reinforcement adjustments.", 8.25m, new Guid("2f99d8d5-2f8b-4d24-9781-1f1f7ad7d104") },
                { new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68005"), new DateOnly(2026, 3, 19), "Supervised framing crew and reorganized the sequencing for facade preparation.", 9.00m, new Guid("dbf81307-b91f-49f1-8ab5-46cd7b7ed105") },
                { new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68006"), new DateOnly(2026, 3, 19), "Managed inbound material delivery and updated inventory for steel anchors.", 7.75m, new Guid("f44da1bc-441f-4de3-b2f4-a9798db17a06") },
                { new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68007"), new DateOnly(2026, 3, 18), "Morning coordination meeting and afternoon follow-up on concrete curing status.", 8.50m, new Guid("6d4c130b-d3ec-4da3-a7c3-f8ff5df74f01") },
                { new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68008"), new DateOnly(2026, 3, 18), "Earthmoving tasks completed for temporary drainage channel on the east side.", 8.00m, new Guid("9a9d3e2e-bf2e-4aa7-a714-bdd4db98a102") },
                { new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68009"), new DateOnly(2026, 3, 17), "Closed safety observations from the week and briefed crew leaders on access control.", 7.00m, new Guid("45df4f81-bd59-46b0-9df5-baa8543dca03") },
                { new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68010"), new DateOnly(2026, 3, 17), "Updated technical drawings and checked slab opening dimensions with the field team.", 8.00m, new Guid("2f99d8d5-2f8b-4d24-9781-1f1f7ad7d104") },
                { new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68011"), new DateOnly(2026, 3, 16), "Led brickwork coordination and resolved sequencing conflict with electrical conduits.", 8.75m, new Guid("dbf81307-b91f-49f1-8ab5-46cd7b7ed105") },
                { new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68012"), new DateOnly(2026, 3, 15), "Processed urgent supplier replacement and reorganized protected storage area.", 6.50m, new Guid("f44da1bc-441f-4de3-b2f4-a9798db17a06") }
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(table: "dailyReports", keyColumn: "Id", keyValue: new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68001"));
        migrationBuilder.DeleteData(table: "dailyReports", keyColumn: "Id", keyValue: new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68002"));
        migrationBuilder.DeleteData(table: "dailyReports", keyColumn: "Id", keyValue: new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68003"));
        migrationBuilder.DeleteData(table: "dailyReports", keyColumn: "Id", keyValue: new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68004"));
        migrationBuilder.DeleteData(table: "dailyReports", keyColumn: "Id", keyValue: new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68005"));
        migrationBuilder.DeleteData(table: "dailyReports", keyColumn: "Id", keyValue: new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68006"));
        migrationBuilder.DeleteData(table: "dailyReports", keyColumn: "Id", keyValue: new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68007"));
        migrationBuilder.DeleteData(table: "dailyReports", keyColumn: "Id", keyValue: new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68008"));
        migrationBuilder.DeleteData(table: "dailyReports", keyColumn: "Id", keyValue: new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68009"));
        migrationBuilder.DeleteData(table: "dailyReports", keyColumn: "Id", keyValue: new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68010"));
        migrationBuilder.DeleteData(table: "dailyReports", keyColumn: "Id", keyValue: new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68011"));
        migrationBuilder.DeleteData(table: "dailyReports", keyColumn: "Id", keyValue: new Guid("2d61957a-7095-4bd7-bf22-0f6ee4d68012"));

        migrationBuilder.DeleteData(table: "incidents", keyColumn: "Id", keyValue: new Guid("778f5d0e-f8d8-4b70-a203-9161c8a89001"));
        migrationBuilder.DeleteData(table: "incidents", keyColumn: "Id", keyValue: new Guid("778f5d0e-f8d8-4b70-a203-9161c8a89002"));
        migrationBuilder.DeleteData(table: "incidents", keyColumn: "Id", keyValue: new Guid("778f5d0e-f8d8-4b70-a203-9161c8a89003"));
        migrationBuilder.DeleteData(table: "incidents", keyColumn: "Id", keyValue: new Guid("778f5d0e-f8d8-4b70-a203-9161c8a89004"));
        migrationBuilder.DeleteData(table: "incidents", keyColumn: "Id", keyValue: new Guid("778f5d0e-f8d8-4b70-a203-9161c8a89005"));
        migrationBuilder.DeleteData(table: "incidents", keyColumn: "Id", keyValue: new Guid("778f5d0e-f8d8-4b70-a203-9161c8a89006"));

        migrationBuilder.DeleteData(table: "workers", keyColumn: "Id", keyValue: new Guid("2f99d8d5-2f8b-4d24-9781-1f1f7ad7d104"));
        migrationBuilder.DeleteData(table: "workers", keyColumn: "Id", keyValue: new Guid("dbf81307-b91f-49f1-8ab5-46cd7b7ed105"));
        migrationBuilder.DeleteData(table: "workers", keyColumn: "Id", keyValue: new Guid("f44da1bc-441f-4de3-b2f4-a9798db17a06"));
        migrationBuilder.DeleteData(table: "workers", keyColumn: "Id", keyValue: new Guid("8ab7e0ff-e769-490f-b503-96b7c6ef5d07"));
    }
}
