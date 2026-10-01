using FlowDesk.Api.DTOs;

namespace FlowDesk.Api.Services.Interface
{
    public interface IWarehouseService
    {
        Task<List<WarehouseDto>> GetAllAsync();

        Task<WarehouseDto?> GetByIdAsync(int id);

        Task<WarehouseDto> CreateAsync(CreateWarehouseDto dto);
    }
}
