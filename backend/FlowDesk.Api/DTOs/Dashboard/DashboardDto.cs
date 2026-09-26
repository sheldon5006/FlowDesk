namespace FlowDesk.Api.DTOs.Dashboard
{
    public class DashboardDto
    {
    }
    public class DashboardSummaryDto
    {
        public int TotalEmployees { get; set; }

        public int UpcomingShifts { get; set; }

        public int PendingAssignments { get; set; }

        public int UnfilledPositions { get; set; }
    }
}
