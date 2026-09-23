using FlowDesk.Api.Data;
using FlowDesk.Api.DTOs;
using FlowDesk.Api.Models;
using FlowDesk.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FlowDesk.Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly FlowDeskDbContext _db;

        public EmployeeService(FlowDeskDbContext db)
        {
            _db = db;
        }

        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            return await _db.Employees
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

        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            _db.Employees.Add(employee);

            await _db.SaveChangesAsync();

            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email
            };
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            return await _db.Employees
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

    }
}
