using FlowDesk.Api.Data;
using FlowDesk.Api.DTOs;
using FlowDesk.Api.Models;
using FlowDesk.Api.Repositories;
using FlowDesk.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;


namespace FlowDesk.Api.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IRepository<Assignment> _assignmentRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Shift> _shiftRepository;

        public AssignmentService(
            IRepository<Assignment> assignmentRepository,
            IRepository<Employee> employeeRepository,
            IRepository<Shift> shiftRepository)
        {
            _assignmentRepository = assignmentRepository;
            _employeeRepository = employeeRepository;
            _shiftRepository = shiftRepository;
        }

        public async Task<AssignmentDto?> CreateAsync(CreateAssignmentDto dto)
        {
            var employee = await _employeeRepository
                .GetByIdAsync(dto.EmployeeId);

            if (employee == null)
            {
                return null;
            }

            var shift = await _shiftRepository
                .GetByIdAsync(dto.ShiftId);

            if (shift == null)
            {
                return null;
            }

            if (shift.Status == ShiftStatus.Cancelled ||
                shift.Status == ShiftStatus.Completed)
            {
                throw new InvalidOperationException(
                    "Assignments cannot be added to a cancelled or completed shift.");
            }

            var activeAssignmentCount = await _assignmentRepository
                .Query()
                .CountAsync(a =>
                    a.ShiftId == dto.ShiftId &&
                    a.Status != AssignmentStatus.Cancelled);

            if (activeAssignmentCount >= shift.RequiredEmployees)
            {
                throw new InvalidOperationException(
                    "This shift has already reached its required employee capacity.");
            }

            var existingAssignment = await _assignmentRepository
                .Query()
                .FirstOrDefaultAsync(a =>
                    a.EmployeeId == dto.EmployeeId &&
                    a.ShiftId == dto.ShiftId &&
                    a.Status != AssignmentStatus.Cancelled);

            if (existingAssignment != null)
            {
                throw new InvalidOperationException(
                    "Employee is already assigned to this shift.");
            }

            var assignment = new Assignment
            {
                EmployeeId = dto.EmployeeId,
                ShiftId = dto.ShiftId,
                Status = AssignmentStatus.Pending,
                AssignedAt = DateTime.UtcNow
            };

            await _assignmentRepository.AddAsync(assignment);

            await _assignmentRepository.SaveChangesAsync();

            return await GetByIdAsync(assignment.Id);
        }

        public async Task<AssignmentDto?> GetByIdAsync(int id)
        {
            return await _assignmentRepository
                .Query()
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => new AssignmentDto
                {
                    Id = a.Id,
                    EmployeeId = a.EmployeeId,
                    EmployeeName = a.Employee.FirstName + " " + a.Employee.LastName,
                    ShiftId = a.ShiftId,
                    Status = a.Status,
                    AssignedAt = a.AssignedAt,
                    ShiftDate = a.Shift.Date,

                    StartTime = a.Shift.StartTime,

                    EndTime = a.Shift.EndTime,

                    Location = a.Shift.Location,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<AssignmentDto>> GetByShiftAsync(int shiftId)
        {
            return await _assignmentRepository
                .Query()
                .AsNoTracking()
                .Where(a => a.ShiftId == shiftId)
                .Select(a => new AssignmentDto
                {
                    Id = a.Id,
                    EmployeeId = a.EmployeeId,
                    EmployeeName = a.Employee.FirstName + " " + a.Employee.LastName,
                    ShiftId = a.ShiftId,
                    Status = a.Status,
                    AssignedAt = a.AssignedAt,
                    ShiftDate = a.Shift.Date,

                    StartTime = a.Shift.StartTime,

                    EndTime = a.Shift.EndTime,

                    Location = a.Shift.Location,
                })
                .ToListAsync();
        }

        public async Task<List<AssignmentDto>> GetByEmployeeAsync(int employeeId)
        {
            return await _assignmentRepository
                .Query()
                .AsNoTracking()
                .Where(a => a.EmployeeId == employeeId)
                .Select(a => new AssignmentDto
                {
                    Id = a.Id,
                    EmployeeId = a.EmployeeId,
                    EmployeeName = a.Employee.FirstName + " " + a.Employee.LastName,
                    ShiftId = a.ShiftId,
                    Status = a.Status,
                    AssignedAt = a.AssignedAt,
                    ShiftDate = a.Shift.Date,

                    StartTime = a.Shift.StartTime,

                    EndTime = a.Shift.EndTime,

                    Location = a.Shift.Location,
                })
                .ToListAsync();
        }

        public async Task<bool> ConfirmAsync(int id)
        {
            var assignment = await _assignmentRepository
                            .Query()
                            .Include(a => a.Shift)
                            .FirstOrDefaultAsync(a => a.Id == id);

            if (assignment == null)
            {
                return false;
            }

            if (assignment.Shift.Status != ShiftStatus.Published)
            {
                throw new InvalidOperationException(
                    "Only assignments for published shifts can be confirmed.");
            }

            assignment.Status = AssignmentStatus.Confirmed;

            _assignmentRepository.Update(assignment);

            await _assignmentRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(id);

            if (assignment == null)
            {
                return false;
            }

            _assignmentRepository.Delete(assignment);

            await _assignmentRepository.SaveChangesAsync();

            return true;
        }

        public async Task<List<AssignmentDto>> GetAllAsync()
        {
            return await _assignmentRepository
                .Query()
                .AsNoTracking()
                .Select(a => new AssignmentDto
                {
                    Id = a.Id,

                    EmployeeId = a.EmployeeId,

                    EmployeeName =
                        a.Employee.FirstName + " " +
                        a.Employee.LastName,

                    ShiftId = a.ShiftId,

                    ShiftDate = a.Shift.Date,

                    StartTime = a.Shift.StartTime,

                    EndTime = a.Shift.EndTime,

                    Location = a.Shift.Location,

                    Status = a.Status,

                    AssignedAt = a.AssignedAt
                })
                .ToListAsync();
        }
    }
}
