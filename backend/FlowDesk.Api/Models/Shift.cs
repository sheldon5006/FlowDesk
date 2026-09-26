namespace FlowDesk.Api.Models
{
    public class Shift
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string Location { get; set; } = string.Empty;

        public int RequiredEmployees { get; set; }

        public ShiftStatus Status { get; set; } = ShiftStatus.Draft;

        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}
