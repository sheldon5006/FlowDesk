using FlowDesk.Api.DTOs.Shifts;

namespace FlowDesk.Api.Services.Interface
{
    public interface IShiftService
    {
        Task<List<ShiftDto>> GetAllAsync();

        Task<ShiftDto?> GetByIdAsync(int id);

        Task<ShiftDto> CreateAsync(CreateShiftDto dto);
        Task<bool> PublishAsync(int id);

        Task<bool> CompleteAsync(int id);

        Task<bool> CancelAsync(int id);
    }
}
