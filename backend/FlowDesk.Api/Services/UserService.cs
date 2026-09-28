using FlowDesk.Api.DTOs;
using FlowDesk.Api.Models;
using FlowDesk.Api.Repositories;
using FlowDesk.Api.Services.Interface;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;

namespace FlowDesk.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(
            IRepository<User> userRepository,
            IRepository<Employee> employeeRepository,
            IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<List<UsersDto>> GetAllAsync()
        {
            return await _userRepository
                .Query()
                .AsNoTracking()
                .Select(x => new UsersDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    Role = x.Role,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.Employee != null
                        ? x.Employee.FirstName + " " + x.Employee.LastName
                        : null
                })
                .ToListAsync();
        }

        public async Task<UsersDto?> GetByIdAsync(int id)
        {
            return await _userRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new UsersDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    Role = x.Role,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.Employee != null
                        ? x.Employee.FirstName + " " + x.Employee.LastName
                        : null
                })
                .FirstOrDefaultAsync();
        }

        public async Task<UsersDto> CreateAsync(CreateUserDto dto)
        {
            var email = dto.Email.Trim().ToLower();

            var existingUser = await _userRepository
                .Query()
                .AnyAsync(x => x.Email == email);

            if (existingUser)
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }

            if (dto.EmployeeId.HasValue)
            {
                var employee = await _employeeRepository
                    .GetByIdAsync(dto.EmployeeId.Value);

                if (employee == null)
                {
                    throw new InvalidOperationException("Employee does not exist.");
                }
            }

            var user = new User
            {
                Email = email,
                Role = dto.Role,
                EmployeeId = dto.EmployeeId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(
                user,
                dto.Password);

            await _userRepository.AddAsync(user);

            await _userRepository.SaveChangesAsync();

            return new UsersDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                EmployeeId = user.EmployeeId
            };
        }
    }
}
