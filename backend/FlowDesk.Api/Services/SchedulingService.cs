using FlowDesk.Api.DTOs.Scheduling;
using FlowDesk.Api.Models;
using FlowDesk.Api.Repositories;
using FlowDesk.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;


namespace FlowDesk.Api.Services
{
    public class SchedulingService : ISchedulingService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Shift> _shiftRepository;
        private readonly IRepository<Assignment> _assignmentRepository;

        public SchedulingService(
            IRepository<Employee> employeeRepository,
            IRepository<Shift> shiftRepository,
            IRepository<Assignment> assignmentRepository)
        {
            _employeeRepository = employeeRepository;
            _shiftRepository = shiftRepository;
            _assignmentRepository = assignmentRepository;
        }

        public async Task<List<SchedulingCandidateDto>> GetCandidatesAsync(int shiftId)
        {
            var shift = await _shiftRepository.GetByIdAsync(shiftId);

            if (shift == null)
            {
                return new List<SchedulingCandidateDto>();
            }

            var weekStart = shift.Date.AddDays( -(int)shift.Date.DayOfWeek + 1);

            if (shift.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                weekStart = shift.Date.AddDays(-6);
            }

            var weekEnd = weekStart.AddDays(6);

            var eligibleEmployees = await _employeeRepository
                                    .Query()
                                    .AsNoTracking()
                                    .Where(e =>
                                        e.Availabilities.Any(a =>
                                            a.DayOfWeek == shift.Date.DayOfWeek &&
                                            a.StartTime <= shift.StartTime &&
                                            a.EndTime >= shift.EndTime
                                        )
                                        &&
                                        !e.Assignments.Any(a =>
                                            a.Status != AssignmentStatus.Cancelled &&
                                            a.Shift.Date == shift.Date &&
                                            a.Shift.StartTime < shift.EndTime &&
                                            a.Shift.EndTime > shift.StartTime
                                        )
                                    )
                                    .Select(e => new
                                    {
                                        e.Id,
                                        e.FirstName,
                                        e.LastName,
                                        e.Email
                                    })
                                    .ToListAsync();

            var employeeIds = eligibleEmployees
                            .Select(e => e.Id)
                            .ToList();

            var weeklyAssignments = await _assignmentRepository
                                    .Query()
                                    .AsNoTracking()
                                    .Where(a =>
                                        employeeIds.Contains(a.EmployeeId) &&
                                        a.Status != AssignmentStatus.Cancelled &&
                                        a.Shift.Date >= weekStart &&
                                        a.Shift.Date <= weekEnd)
                                    .Select(a => new
                                    {
                                        a.EmployeeId,
                                        a.Shift.StartTime,
                                        a.Shift.EndTime
                                    })
                                    .ToListAsync();

            var candidates = eligibleEmployees
                .Select(employee =>
                {
                    var employeeAssignments = weeklyAssignments
                        .Where(a => a.EmployeeId == employee.Id);

                    var totalHours = employeeAssignments.Sum(a =>
                        (a.EndTime.ToTimeSpan() - a.StartTime.ToTimeSpan())
                            .TotalHours);

                    return new SchedulingCandidateDto
                    {
                        EmployeeId = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Email = employee.Email,
                        AssignedHoursThisWeek = totalHours
                    };
                })
                .OrderBy(c => c.AssignedHoursThisWeek)
                .ToList();

            return candidates;
        }
    }
}
