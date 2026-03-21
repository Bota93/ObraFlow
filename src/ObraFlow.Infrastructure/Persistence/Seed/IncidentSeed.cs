using ObraFlow.Domain.Entities;
using ObraFlow.Domain.Enums;

namespace ObraFlow.Infrastructure.Persistence.Seed;

public static class IncidentSeed
{
    public static readonly Incident[] Data =
    [
        new Incident
        {
            Id = Guid.Parse("778f5d0e-f8d8-4b70-a203-9161c8a89001"),
            Title = "Water ingress in basement corridor",
            Description = "Water accumulation was detected near the service corridor after overnight rain.",
            Status = IncidentStatus.Open,
            ReportedAtUtc = new DateTime(2026, 3, 21, 7, 45, 0, DateTimeKind.Utc)
        },
        new Incident
        {
            Id = Guid.Parse("778f5d0e-f8d8-4b70-a203-9161c8a89002"),
            Title = "Scaffold access gate damaged",
            Description = "The access gate on the west scaffold bay requires replacement before next shift.",
            Status = IncidentStatus.InProgress,
            ReportedAtUtc = new DateTime(2026, 3, 20, 9, 20, 0, DateTimeKind.Utc)
        },
        new Incident
        {
            Id = Guid.Parse("778f5d0e-f8d8-4b70-a203-9161c8a89003"),
            Title = "Concrete delivery delay",
            Description = "Morning concrete pour was delayed due to a blocked supplier route and was rescheduled.",
            Status = IncidentStatus.Resolved,
            ReportedAtUtc = new DateTime(2026, 3, 19, 6, 50, 0, DateTimeKind.Utc)
        },
        new Incident
        {
            Id = Guid.Parse("778f5d0e-f8d8-4b70-a203-9161c8a89004"),
            Title = "PPE shortage in finishing crew",
            Description = "Several replacement helmets and gloves are pending for the finishing subcontractor.",
            Status = IncidentStatus.InProgress,
            ReportedAtUtc = new DateTime(2026, 3, 18, 12, 15, 0, DateTimeKind.Utc)
        },
        new Incident
        {
            Id = Guid.Parse("778f5d0e-f8d8-4b70-a203-9161c8a89005"),
            Title = "Lighting failure in storage area",
            Description = "Temporary lighting failed in the protected storage area, reducing visibility for late shifts.",
            Status = IncidentStatus.Open,
            ReportedAtUtc = new DateTime(2026, 3, 17, 17, 30, 0, DateTimeKind.Utc)
        },
        new Incident
        {
            Id = Guid.Parse("778f5d0e-f8d8-4b70-a203-9161c8a89006"),
            Title = "Minor forklift collision with barriers",
            Description = "A forklift touched the temporary barriers at low speed, with no injuries reported.",
            Status = IncidentStatus.Resolved,
            ReportedAtUtc = new DateTime(2026, 3, 16, 10, 5, 0, DateTimeKind.Utc)
        },
    ];
}
