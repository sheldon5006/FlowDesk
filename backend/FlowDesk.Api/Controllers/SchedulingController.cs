using FlowDesk.Api.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FlowDesk.Api.Controllers
{
    [ApiController]
    [Route("api/scheduling")]
    public class SchedulingController : ControllerBase
    {
        private readonly ISchedulingService _schedulingService;

        public SchedulingController(ISchedulingService schedulingService)
        {
            _schedulingService = schedulingService;
        }

        [HttpGet("shifts/{shiftId}/candidates")]
        public async Task<IActionResult> GetCandidates(int shiftId)
        {
            var candidates =
                await _schedulingService.GetCandidatesAsync(shiftId);

            return Ok(candidates);
        }
    }
}
