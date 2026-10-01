using FlowDesk.Api.DTOs;
using FlowDesk.Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowDesk.Api.Controllers
{
    [ApiController]
    [Route("api/warehouses")]
    [Authorize(Roles = "Admin")]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehousesController(
            IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var warehouses =
                await _warehouseService.GetAllAsync();

            return Ok(warehouses);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var warehouse =
                await _warehouseService.GetByIdAsync(id);

            if (warehouse is null)
            {
                return NotFound();
            }

            return Ok(warehouse);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateWarehouseDto dto)
        {
            try
            {
                var warehouse =
                    await _warehouseService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = warehouse.Id },
                    warehouse);
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