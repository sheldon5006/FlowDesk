using FlowDesk.Api.DTOs;
using FlowDesk.Api.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FlowDesk.Api.Controllers
{

    [ApiController]
    [Route("api/assignments")]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentsController(
            IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAssignmentDto dto)
        {
            try
            {
                var result = await _assignmentService.CreateAsync(dto);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("shift/{shiftId:int}")]
        public async Task<IActionResult> GetByShift(int shiftId)
        {
            var assignments =
                await _assignmentService.GetByShiftAsync(shiftId);

            return Ok(assignments);
        }

        [HttpGet("employee/{employeeId:int}")]
        public async Task<IActionResult> GetByEmployee(
            int employeeId)
        {
            var assignments =
                await _assignmentService.GetByEmployeeAsync(employeeId);

            return Ok(assignments);
        }

        [HttpPut("{assignmentId:int}/confirm")]
        public async Task<IActionResult> Confirm(int assignmentId)
        {
            try
            {
                var assignment =
                    await _assignmentService.ConfirmAsync(assignmentId);

                if (assignment is false)
                {
                    return NotFound();
                }

                return Ok(assignment);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpDelete("{assignmentId:int}")]
        public async Task<IActionResult> Delete(int assignmentId)
        {
            var deleted =
                await _assignmentService.DeleteAsync(assignmentId);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var assignments =
                await _assignmentService.GetAllAsync();

            return Ok(assignments);
        }
    }
}
