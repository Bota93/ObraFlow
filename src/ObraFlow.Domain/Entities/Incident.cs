using ObraFlow.Domain.Enums;

namespace ObraFlow.Domain.Entities;

public class Incident
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IncidentStatus Status { get; set; } = IncidentStatus.Open;
    public DateTime ReportedAtUtc { get; set; }
}
