using FlowDesk.Api.DTOs.Scheduling;

namespace FlowDesk.Api.Services.Interface
{
    public interface ISchedulingService
    {
        Task<List<SchedulingCandidateDto>> GetCandidatesAsync(int shiftId);
    }
}
