namespace FlowDesk.Api.Models
{
    public class Assignment
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int ShiftId { get; set; }

        public AssignmentStatus Status { get; set; }
            = AssignmentStatus.Pending;

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        public Employee Employee { get; set; } = null!;

        public Shift Shift { get; set; } = null!;
    }
}
