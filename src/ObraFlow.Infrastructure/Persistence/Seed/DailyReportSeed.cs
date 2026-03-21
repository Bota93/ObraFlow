using ObraFlow.Domain.Entities;

namespace ObraFlow.Infrastructure.Persistence.Seed;

public static class DailyReportSeed
{
    public static readonly DailyReport[] Data =
    [
        new DailyReport
        {
            Id = Guid.Parse("2d61957a-7095-4bd7-bf22-0f6ee4d68001"),
            Date = new DateOnly(2026, 3, 21),
            WorkerId = Guid.Parse("6d4c130b-d3ec-4da3-a7c3-f8ff5df74f01"),
            HoursWorked = 8.00m,
            Description = "Coordinated subcontractors and reviewed progress in the structural zone."
        },
        new DailyReport
        {
            Id = Guid.Parse("2d61957a-7095-4bd7-bf22-0f6ee4d68002"),
            Date = new DateOnly(2026, 3, 21),
            WorkerId = Guid.Parse("9a9d3e2e-bf2e-4aa7-a714-bdd4db98a102"),
            HoursWorked = 7.50m,
            Description = "Excavation support and machinery checks completed near the north access."
        },
        new DailyReport
        {
            Id = Guid.Parse("2d61957a-7095-4bd7-bf22-0f6ee4d68003"),
            Date = new DateOnly(2026, 3, 20),
            WorkerId = Guid.Parse("45df4f81-bd59-46b0-9df5-baa8543dca03"),
            HoursWorked = 8.00m,
            Description = "Performed safety inspection and updated risk signage after equipment movement."
        },
        new DailyReport
        {
            Id = Guid.Parse("2d61957a-7095-4bd7-bf22-0f6ee4d68004"),
            Date = new DateOnly(2026, 3, 20),
            WorkerId = Guid.Parse("2f99d8d5-2f8b-4d24-9781-1f1f7ad7d104"),
            HoursWorked = 8.25m,
            Description = "Reviewed concrete pour schedule and approved reinforcement adjustments."
        },
        new DailyReport
        {
            Id = Guid.Parse("2d61957a-7095-4bd7-bf22-0f6ee4d68005"),
            Date = new DateOnly(2026, 3, 19),
            WorkerId = Guid.Parse("dbf81307-b91f-49f1-8ab5-46cd7b7ed105"),
            HoursWorked = 9.00m,
            Description = "Supervised framing crew and reorganized the sequencing for facade preparation."
        },
        new DailyReport
        {
            Id = Guid.Parse("2d61957a-7095-4bd7-bf22-0f6ee4d68006"),
            Date = new DateOnly(2026, 3, 19),
            WorkerId = Guid.Parse("f44da1bc-441f-4de3-b2f4-a9798db17a06"),
            HoursWorked = 7.75m,
            Description = "Managed inbound material delivery and updated inventory for steel anchors."
        },
        new DailyReport
        {
            Id = Guid.Parse("2d61957a-7095-4bd7-bf22-0f6ee4d68007"),
            Date = new DateOnly(2026, 3, 18),
            WorkerId = Guid.Parse("6d4c130b-d3ec-4da3-a7c3-f8ff5df74f01"),
            HoursWorked = 8.50m,
            Description = "Morning coordination meeting and afternoon follow-up on concrete curing status."
        },
        new DailyReport
        {
            Id = Guid.Parse("2d61957a-7095-4bd7-bf22-0f6ee4d68008"),
            Date = new DateOnly(2026, 3, 18),
            WorkerId = Guid.Parse("9a9d3e2e-bf2e-4aa7-a714-bdd4db98a102"),
            HoursWorked = 8.00m,
            Description = "Earthmoving tasks completed for temporary drainage channel on the east side."
        },
        new DailyReport
        {
            Id = Guid.Parse("2d61957a-7095-4bd7-bf22-0f6ee4d68009"),
            Date = new DateOnly(2026, 3, 17),
            WorkerId = Guid.Parse("45df4f81-bd59-46b0-9df5-baa8543dca03"),
            HoursWorked = 7.00m,
            Description = "Closed safety observations from the week and briefed crew leaders on access control."
        },
        new DailyReport
        {
            Id = Guid.Parse("2d61957a-7095-4bd7-bf22-0f6ee4d68010"),
            Date = new DateOnly(2026, 3, 17),
            WorkerId = Guid.Parse("2f99d8d5-2f8b-4d24-9781-1f1f7ad7d104"),
            HoursWorked = 8.00m,
            Description = "Updated technical drawings and checked slab opening dimensions with the field team."
        },
        new DailyReport
        {
            Id = Guid.Parse("2d61957a-7095-4bd7-bf22-0f6ee4d68011"),
            Date = new DateOnly(2026, 3, 16),
            WorkerId = Guid.Parse("dbf81307-b91f-49f1-8ab5-46cd7b7ed105"),
            HoursWorked = 8.75m,
            Description = "Led brickwork coordination and resolved sequencing conflict with electrical conduits."
        },
        new DailyReport
        {
            Id = Guid.Parse("2d61957a-7095-4bd7-bf22-0f6ee4d68012"),
            Date = new DateOnly(2026, 3, 15),
            WorkerId = Guid.Parse("f44da1bc-441f-4de3-b2f4-a9798db17a06"),
            HoursWorked = 6.50m,
            Description = "Processed urgent supplier replacement and reorganized protected storage area."
        },
    ];
}
