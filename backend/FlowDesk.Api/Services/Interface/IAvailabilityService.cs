using FlowDesk.Api.DTOs;

namespace FlowDesk.Api.Services.Interface
{
    public interface IAvailabilityService
    {
        Task<List<AvailabilityDto>> GetByEmployeeAsync(int employeeId);

        Task<AvailabilityDto?> CreateAsync(
            int employeeId,
            CreateAvailabilityDto dto);

        Task<bool> DeleteAsync(int employeeId, int id);
    }
}
