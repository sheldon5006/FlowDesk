using FlowDesk.Api.DTOs;
using FlowDesk.Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowDesk.Api.Controllers
{
    [ApiController]
    [Route("api/warehouses/{warehouseId:int}/inventory")]
    [Authorize(Roles = "Admin")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(
            IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int warehouseId)
        {
            var inventory =
                await _inventoryService
                    .GetByWarehouseAsync(warehouseId);

            return Ok(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            int warehouseId,
            CreateInventoryDto dto)
        {
            try
            {
                var inventory =
                    await _inventoryService.CreateAsync(
                        warehouseId,
                        dto);

                return Ok(inventory);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }
    }
}