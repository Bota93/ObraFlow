using ObraFlow.Domain.Enums;

namespace ObraFlow.Application.DTOs.Incidents;

public class IncidentDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IncidentStatus Status { get; set; }
    public DateTime ReportedAtUtc { get; set; }
}