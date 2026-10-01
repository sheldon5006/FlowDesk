using FlowDesk.Api.DTOs;
using FlowDesk.Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowDesk.Api.Controllers
{
    [ApiController]
    [Route("api/warehouses/{warehouseId:int}/locations")]
    [Authorize(Roles = "Admin")]
    public class WarehouseLocationsController
        : ControllerBase
    {
        private readonly IWarehouseLocationService
            _locationService;

        public WarehouseLocationsController(
            IWarehouseLocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int warehouseId)
        {
            var locations =
                await _locationService
                    .GetByWarehouseAsync(warehouseId);

            return Ok(locations);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            int warehouseId,
            CreateWarehouseLocationDto dto)
        {
            try
            {
                var location =
                    await _locationService.CreateAsync(
                        warehouseId,
                        dto);

                return Ok(location);
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