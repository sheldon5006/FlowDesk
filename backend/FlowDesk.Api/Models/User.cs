namespace FlowDesk.Api.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.Employee;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? EmployeeId { get; set; }

        public Employee? Employee { get; set; }
    }
}
