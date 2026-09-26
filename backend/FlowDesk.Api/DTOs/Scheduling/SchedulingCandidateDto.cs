namespace FlowDesk.Api.DTOs.Scheduling
{
    public class SchedulingCandidateDto
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public double AssignedHoursThisWeek { get; set; }
    }
}
