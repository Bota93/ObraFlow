using ObraFlow.Domain.Entities;

namespace ObraFlow.Infrastructure.Persistence.Seed;

public static class WorkerSeed
{
    public static readonly Worker[] Data =
    [
        new Worker
        {
            Id = Guid.Parse("6d4c130b-d3ec-4da3-a7c3-f8ff5df74f01"),
            Name = "Miguel Torres",
            Role = "Site Supervisor",
            PhoneNumber = "+34 600 111 222",
            HourlyRate = 34.50m,
            CreatedAtUtc = new DateTime(2026, 3, 1, 8, 0, 0, DateTimeKind.Utc),
            IsActive = true,
        },
        new Worker
        {
            Id = Guid.Parse("9a9d3e2e-bf2e-4aa7-a714-bdd4db98a102"),
            Name = "Laura Martinez",
            Role = "Heavy Equipment Operator",
            PhoneNumber = "+34 600 333 444",
            HourlyRate = 28.75m,
            CreatedAtUtc = new DateTime(2026, 3, 2, 8, 0, 0, DateTimeKind.Utc),
            IsActive = true,
        },
        new Worker
        {
            Id = Guid.Parse("45df4f81-bd59-46b0-9df5-baa8543dca03"),
            Name = "Carlos Ramirez",
            Role = "Safety Technician",
            PhoneNumber = "+34 600 555 666",
            HourlyRate = 26.25m,
            CreatedAtUtc = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc),
            IsActive = true,
        },
        new Worker
        {
            Id = Guid.Parse("2f99d8d5-2f8b-4d24-9781-1f1f7ad7d104"),
            Name = "Sofia Navarro",
            Role = "Project Engineer",
            PhoneNumber = "+34 600 777 888",
            HourlyRate = 31.90m,
            CreatedAtUtc = new DateTime(2026, 3, 4, 8, 0, 0, DateTimeKind.Utc),
            IsActive = true,
        },
        new Worker
        {
            Id = Guid.Parse("dbf81307-b91f-49f1-8ab5-46cd7b7ed105"),
            Name = "Diego Perez",
            Role = "Foreman",
            PhoneNumber = "+34 600 999 000",
            HourlyRate = 29.80m,
            CreatedAtUtc = new DateTime(2026, 3, 5, 8, 0, 0, DateTimeKind.Utc),
            IsActive = true,
        },
        new Worker
        {
            Id = Guid.Parse("f44da1bc-441f-4de3-b2f4-a9798db17a06"),
            Name = "Elena Castro",
            Role = "Logistics Coordinator",
            PhoneNumber = "+34 611 111 222",
            HourlyRate = 24.60m,
            CreatedAtUtc = new DateTime(2026, 3, 6, 8, 0, 0, DateTimeKind.Utc),
            IsActive = true,
        },
        new Worker
        {
            Id = Guid.Parse("8ab7e0ff-e769-490f-b503-96b7c6ef5d07"),
            Name = "Javier Ortega",
            Role = "Masonry Specialist",
            PhoneNumber = "+34 611 333 444",
            HourlyRate = 27.40m,
            CreatedAtUtc = new DateTime(2026, 3, 7, 8, 0, 0, DateTimeKind.Utc),
            IsActive = false,
        },
    ];
}
