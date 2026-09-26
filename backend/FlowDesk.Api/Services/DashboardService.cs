using FlowDesk.Api.Models;
using FlowDesk.Api.Repositories;
using FlowDesk.Api.Services.Interface;
using FlowDesk.Api.DTOs.Dashboard;

using Microsoft.EntityFrameworkCore;


namespace FlowDesk.Api.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Shift> _shiftRepository;
        private readonly IRepository<Assignment> _assignmentRepository;

        public DashboardService(
            IRepository<Employee> employeeRepository,
            IRepository<Shift> shiftRepository,
            IRepository<Assignment> assignmentRepository)
        {
            _employeeRepository = employeeRepository;
            _shiftRepository = shiftRepository;
            _assignmentRepository = assignmentRepository;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            var totalEmployees = await _employeeRepository
                .Query()
                .CountAsync();

            var upcomingShifts = await _shiftRepository
                .Query()
                .CountAsync(s =>
                    s.Date >= today &&
                    s.Status != ShiftStatus.Completed &&
                    s.Status != ShiftStatus.Cancelled);

            var pendingAssignments = await _assignmentRepository
                .Query()
                .CountAsync(a =>
                    a.Status == AssignmentStatus.Pending);

            var upcomingShiftData = await _shiftRepository
                .Query()
                .Where(s =>
                    s.Date >= today &&
                    s.Status != ShiftStatus.Completed &&
                    s.Status != ShiftStatus.Cancelled)
                .Select(s => new
                {
                    s.RequiredEmployees,
                    AssignedEmployees = s.Assignments
                        .Count(a =>
                            a.Status != AssignmentStatus.Cancelled)
                })
                .ToListAsync();

            var unfilledPositions = upcomingShiftData.Sum(s =>
                Math.Max(
                    0,
                    s.RequiredEmployees - s.AssignedEmployees));

            return new DashboardSummaryDto
            {
                TotalEmployees = totalEmployees,
                UpcomingShifts = upcomingShifts,
                PendingAssignments = pendingAssignments,
                UnfilledPositions = unfilledPositions
            };
        }
    }
}
