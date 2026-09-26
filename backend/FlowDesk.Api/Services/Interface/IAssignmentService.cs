using FlowDesk.Api.DTOs;

namespace FlowDesk.Api.Services.Interface
{
    public interface IAssignmentService
    {
        Task<AssignmentDto?> CreateAsync(CreateAssignmentDto dto);

        Task<AssignmentDto?> GetByIdAsync(int id);

        Task<List<AssignmentDto>> GetByShiftAsync(int shiftId);

        Task<List<AssignmentDto>> GetByEmployeeAsync(int employeeId);

        Task<bool> ConfirmAsync(int id);

        Task<bool> DeleteAsync(int id);
        Task<List<AssignmentDto>> GetAllAsync();
    }
}
