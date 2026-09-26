using FlowDesk.Api.Data;
using FlowDesk.Api.DTOs;
using FlowDesk.Api.Models;
using FlowDesk.Api.Repositories;
using FlowDesk.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FlowDesk.Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            return await _employeeRepository
                .Query()
                .AsNoTracking()
                .Select(x => new EmployeeDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email
                })
                .ToListAsync();
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            return await _employeeRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new EmployeeDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email
                })
                .FirstOrDefaultAsync();
        }

        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            await _employeeRepository.AddAsync(employee);

            await _employeeRepository.SaveChangesAsync();

            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email
            };
        }
    }
}
