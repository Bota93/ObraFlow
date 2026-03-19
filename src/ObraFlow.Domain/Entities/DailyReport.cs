namespace ObraFlow.Domain.Entities
{
    public class DailyReport
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public Guid WorkerId { get; set; }
        public decimal HoursWorked { get; set; }
        public string Description { get; set; } = string.Empty;

        public Worker? Worker { get; set; }
    }
}
