using FlowDesk.Api.Data;
using FlowDesk.Api.DTOs.Shifts;
using FlowDesk.Api.Models;
using FlowDesk.Api.Repositories;
using FlowDesk.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FlowDesk.Api.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IRepository<Shift> _shiftRepository;

        public ShiftService(IRepository<Shift> shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }

        public async Task<List<ShiftDto>> GetAllAsync()
        {
            return await _shiftRepository
                .Query()
                .AsNoTracking()
                .Select(s => new ShiftDto
                {
                    Id = s.Id,
                    Date = s.Date,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    Location = s.Location,
                    RequiredEmployees = s.RequiredEmployees,
                    AssignedEmployeeCount = s.Assignments
                     .Count(a => a.Status != AssignmentStatus.Cancelled),
                    Status = s.Status
                })
                .ToListAsync();
        }

        public async Task<ShiftDto?> GetByIdAsync(int id)
        {
            return await _shiftRepository
                .Query()
                .AsNoTracking()
                .Where(s => s.Id == id)
                .Select(s => new ShiftDto
                {
                    Id = s.Id,
                    Date = s.Date,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    Location = s.Location,
                    RequiredEmployees = s.RequiredEmployees,
                    AssignedEmployeeCount = s.Assignments
        .Count(a => a.Status != AssignmentStatus.Cancelled),
                    Status = s.Status
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ShiftDto> CreateAsync(CreateShiftDto dto)
        {
            // Business rule
            if (dto.EndTime <= dto.StartTime)
            {
                throw new ArgumentException(
                    "End time must be after start time.");
            }

            var shift = new Shift
            {
                Date = dto.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Location = dto.Location,
                RequiredEmployees = dto.RequiredEmployees
            };

            await _shiftRepository.AddAsync(shift);

            await _shiftRepository.SaveChangesAsync();

            return new ShiftDto
            {
                Id = shift.Id,
                Date = shift.Date,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                Location = shift.Location,
                RequiredEmployees = shift.RequiredEmployees,
                AssignedEmployeeCount = 0,
                Status = shift.Status
            };
        }

        public async Task<bool> PublishAsync(int id)
        {
            var shift = await _shiftRepository.GetByIdAsync(id);

            if (shift == null)
            {
                return false;
            }

            if (shift.Status != ShiftStatus.Draft)
            {
                throw new InvalidOperationException(
                    "Only draft shifts can be published.");
            }

            shift.Status = ShiftStatus.Published;

            _shiftRepository.Update(shift);

            await _shiftRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CompleteAsync(int id)
        {
            var shift = await _shiftRepository.GetByIdAsync(id);

            if (shift == null)
            {
                return false;
            }

            if (shift.Status != ShiftStatus.Published)
            {
                throw new InvalidOperationException(
                    "Only published shifts can be completed.");
            }

            shift.Status = ShiftStatus.Completed;

            _shiftRepository.Update(shift);

            await _shiftRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CancelAsync(int id)
        {
            var shift = await _shiftRepository.GetByIdAsync(id);

            if (shift == null)
            {
                return false;
            }

            if (shift.Status == ShiftStatus.Completed ||
                shift.Status == ShiftStatus.Cancelled)
            {
                throw new InvalidOperationException(
                    "This shift can no longer be cancelled.");
            }

            shift.Status = ShiftStatus.Cancelled;

            _shiftRepository.Update(shift);

            await _shiftRepository.SaveChangesAsync();

            return true;
        }
    }
}
