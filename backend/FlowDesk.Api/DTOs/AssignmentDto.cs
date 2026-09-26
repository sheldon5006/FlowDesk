using FlowDesk.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace FlowDesk.Api.DTOs
{
    public class AssignmentDto
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public int ShiftId { get; set; }

        public DateOnly ShiftDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string Location { get; set; } = string.Empty;

        public AssignmentStatus Status { get; set; }

        public DateTime AssignedAt { get; set; }
    }

    public class CreateAssignmentDto
    {
        public int EmployeeId { get; set; }

        public int ShiftId { get; set; }
    }
}
