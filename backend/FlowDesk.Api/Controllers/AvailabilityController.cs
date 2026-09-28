using FlowDesk.Api.DTOs;
using FlowDesk.Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowDesk.Api.Controllers
{
    [ApiController]
    [Route("api/employees/{employeeId:int}/availability")]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;

        public AvailabilityController(
            IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int employeeId)
        {
            var availability =
                await _availabilityService.GetByEmployeeAsync(employeeId);

            return Ok(availability);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int employeeId, CreateAvailabilityDto dto)
        {
            try
            {
                var result = await _availabilityService
                    .CreateAsync(employeeId, dto);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
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

        [HttpDelete("{availabilityId:int}")]
        public async Task<IActionResult> Delete(
            int employeeId,
            int availabilityId)
        {
            var deleted = await _availabilityService.DeleteAsync(
                employeeId,
                availabilityId);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{availabilityId:int}")]
        public async Task<IActionResult> Update(
    int employeeId,
    int availabilityId,
    UpdateAvailabilityDto dto)
        {
            if (!CanAccessEmployee(employeeId))
            {
                return Forbid();
            }

            try
            {
                var result = await _availabilityService.UpdateAsync(
                    employeeId,
                    availabilityId,
                    dto);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
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

        private bool CanAccessEmployee(int employeeId)
        {
            if (User.IsInRole("Admin"))
            {
                return true;
            }

            var employeeIdClaim =
                User.FindFirst("employeeId")?.Value;

            return int.TryParse(
                       employeeIdClaim,
                       out var loggedInEmployeeId)
                   && loggedInEmployeeId == employeeId;
        }
    }
}
