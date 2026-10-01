using FlowDesk.Api.DTOs;

namespace FlowDesk.Api.Services.Interface
{
    public interface IWarehouseLocationService
    {
        Task<List<WarehouseLocationDto>> GetByWarehouseAsync(
            int warehouseId);

        Task<WarehouseLocationDto> CreateAsync(
            int warehouseId,
            CreateWarehouseLocationDto dto);
    }
}