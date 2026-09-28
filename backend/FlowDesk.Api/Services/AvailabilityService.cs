using FlowDesk.Api.Data;
using FlowDesk.Api.DTOs;
using FlowDesk.Api.Models;
using FlowDesk.Api.Repositories;
using FlowDesk.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FlowDesk.Api.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IRepository<EmployeeAvailability> _availabilityRepository;
        private readonly IRepository<Employee> _employeeRepository;

        public AvailabilityService(
            IRepository<EmployeeAvailability> availabilityRepository,
            IRepository<Employee> employeeRepository)
        {
            _availabilityRepository = availabilityRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<List<AvailabilityDto>> GetByEmployeeAsync(int employeeId)
        {
            return await _availabilityRepository
                .Query()
                .AsNoTracking()
                .Where(a => a.EmployeeId == employeeId)
                .Select(a => new AvailabilityDto
                {
                    Id = a.Id,
                    EmployeeId = a.EmployeeId,
                    DayOfWeek = a.DayOfWeek,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime
                })
                .ToListAsync();
        }

        public async Task<AvailabilityDto?> CreateAsync(
            int employeeId,
            CreateAvailabilityDto dto)
        {
            // Business rule 1: employee must exist
            var employee = await _employeeRepository.GetByIdAsync(employeeId);

            if (employee == null)
            {
                return null;
            }

            var existingAvailability = await _availabilityRepository
            .Query()
            .AnyAsync(a =>
                a.EmployeeId == employeeId &&
                a.DayOfWeek == dto.DayOfWeek);

            if (existingAvailability)
            {
                throw new InvalidOperationException(
                    "Availability already exists for this day.");
            }

            // Business rule 2: end time must be after start time
            if (dto.EndTime <= dto.StartTime)
            {
                throw new ArgumentException(
                    "End time must be after start time.");
            }

            var availability = new EmployeeAvailability
            {
                EmployeeId = employeeId,
                DayOfWeek = dto.DayOfWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            await _availabilityRepository.AddAsync(availability);

            await _availabilityRepository.SaveChangesAsync();

            return new AvailabilityDto
            {
                Id = availability.Id,
                EmployeeId = availability.EmployeeId,
                DayOfWeek = availability.DayOfWeek,
                StartTime = availability.StartTime,
                EndTime = availability.EndTime
            };
        }

        public async Task<bool> DeleteAsync(int employeeId, int id)
        {
            var availability = await _availabilityRepository.GetByIdAsync(id);

            if (availability == null)
            {
                return false;
            }

            // Business rule: employee can only delete their own availability
            if (availability.EmployeeId != employeeId)
            {
                return false;
            }

            _availabilityRepository.Delete(availability);

            await _availabilityRepository.SaveChangesAsync();

            return true;
        }

        public async Task<AvailabilityDto?> UpdateAsync(
    int employeeId,
    int availabilityId,
    UpdateAvailabilityDto dto)
        {
            var availability = await _availabilityRepository
                .GetByIdAsync(availabilityId);

            if (availability == null)
            {
                return null;
            }

            // Employee can only update their own availability
            if (availability.EmployeeId != employeeId)
            {
                return null;
            }

            // Business rule: end time must be after start time
            if (dto.EndTime <= dto.StartTime)
            {
                throw new ArgumentException(
                    "End time must be after start time.");
            }

            // Business rule: one availability per employee per day
            var existingAvailability = await _availabilityRepository
                .Query()
                .AnyAsync(a =>
                    a.Id != availabilityId &&
                    a.EmployeeId == employeeId &&
                    a.DayOfWeek == dto.DayOfWeek);

            if (existingAvailability)
            {
                throw new InvalidOperationException(
                    "Availability already exists for this day.");
            }

            availability.DayOfWeek = dto.DayOfWeek;
            availability.StartTime = dto.StartTime;
            availability.EndTime = dto.EndTime;

            _availabilityRepository.Update(availability);

            await _availabilityRepository.SaveChangesAsync();

            return new AvailabilityDto
            {
                Id = availability.Id,
                EmployeeId = availability.EmployeeId,
                DayOfWeek = availability.DayOfWeek,
                StartTime = availability.StartTime,
                EndTime = availability.EndTime
            };
        }
    }
}
