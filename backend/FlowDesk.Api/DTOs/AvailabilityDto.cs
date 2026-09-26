namespace FlowDesk.Api.DTOs
{
    public class AvailabilityDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }

    public class CreateAvailabilityDto
    {
        public DayOfWeek DayOfWeek { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }


}
