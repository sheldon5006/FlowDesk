namespace FlowDesk.Api.Models
{
    public class EmployeeAvailability
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public Employee Employee { get; set; } = null!;
    }
}
