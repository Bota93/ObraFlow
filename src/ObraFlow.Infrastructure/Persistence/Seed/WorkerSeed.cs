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
    ];
}
