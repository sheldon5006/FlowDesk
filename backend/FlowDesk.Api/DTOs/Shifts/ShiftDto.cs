using FlowDesk.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace FlowDesk.Api.DTOs.Shifts
{
    public class ShiftDto
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string Location { get; set; } = string.Empty;

        public int RequiredEmployees { get; set; }
        public int AssignedEmployeeCount { get; set; }
        public ShiftStatus Status { get; set; }
    }

    public class CreateShiftDto
    {
        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string Location { get; set; } = string.Empty;

        public int RequiredEmployees { get; set; }
    }
}
