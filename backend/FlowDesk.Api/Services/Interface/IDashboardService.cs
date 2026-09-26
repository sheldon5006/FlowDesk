using FlowDesk.Api.DTOs.Dashboard;

namespace FlowDesk.Api.Services.Interface
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetSummaryAsync();
    }
}
