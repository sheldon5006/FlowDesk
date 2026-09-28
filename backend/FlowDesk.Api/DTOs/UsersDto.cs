using FlowDesk.Api.Models;

namespace FlowDesk.Api.DTOs
{
    public class UsersDto
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? EmployeeId { get; set; }

        public string? EmployeeName { get; set; }
    }

    public class CreateUserDto
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.Employee;

        public int? EmployeeId { get; set; }
    }

    public class UserListDto
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public bool IsActive { get; set; }

        public int? EmployeeId { get; set; }
    }

    public class UserDetailsDto
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? EmployeeId { get; set; }

        public string? EmployeeName { get; set; }
    }
}
