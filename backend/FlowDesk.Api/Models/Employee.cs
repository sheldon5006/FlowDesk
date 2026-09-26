namespace FlowDesk.Api.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public ICollection<EmployeeAvailability> Availabilities { get; set; } = new List<EmployeeAvailability>();
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    }


}
