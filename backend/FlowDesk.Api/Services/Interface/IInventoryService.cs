using FlowDesk.Api.DTOs;

namespace FlowDesk.Api.Services.Interface
{
    public interface IInventoryService
    {
        Task<List<InventoryDto>> GetByWarehouseAsync(
            int warehouseId);

        Task<InventoryDto> CreateAsync(
            int warehouseId,
            CreateInventoryDto dto);
    }
}