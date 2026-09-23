using FlowDesk.Api.DTOs;
using FlowDesk.Api.Models;

namespace FlowDesk.Api.Services.Interface
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto);
        Task<EmployeeDto?> GetByIdAsync(int id);
    }
}
