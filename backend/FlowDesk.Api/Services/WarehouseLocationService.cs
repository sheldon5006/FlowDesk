using FlowDesk.Api.DTOs;
using FlowDesk.Api.Models;
using FlowDesk.Api.Repositories;
using FlowDesk.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FlowDesk.Api.Services
{
    public class WarehouseLocationService
        : IWarehouseLocationService
    {
        private readonly IRepository<WarehouseLocation>
            _locationRepository;

        private readonly IRepository<Warehouse>
            _warehouseRepository;

        public WarehouseLocationService(
            IRepository<WarehouseLocation> locationRepository,
            IRepository<Warehouse> warehouseRepository)
        {
            _locationRepository = locationRepository;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<List<WarehouseLocationDto>>
            GetByWarehouseAsync(int warehouseId)
        {
            return await _locationRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.WarehouseId == warehouseId)
                .Select(x => new WarehouseLocationDto
                {
                    Id = x.Id,
                    WarehouseId = x.WarehouseId,
                    Name = x.Name,
                    Code = x.Code,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<WarehouseLocationDto> CreateAsync(
            int warehouseId,
            CreateWarehouseLocationDto dto)
        {
            var warehouse = await _warehouseRepository
                .GetByIdAsync(warehouseId);

            if (warehouse == null)
            {
                throw new KeyNotFoundException(
                    "Warehouse not found.");
            }

            var code = dto.Code
                .Trim()
                .ToUpperInvariant();

            var existingLocation = await _locationRepository
                .Query()
                .AnyAsync(x =>
                    x.WarehouseId == warehouseId &&
                    x.Code == code);

            if (existingLocation)
            {
                throw new InvalidOperationException(
                    "A location with this code already exists in this warehouse.");
            }

            var location = new WarehouseLocation
            {
                WarehouseId = warehouseId,
                Name = dto.Name.Trim(),
                Code = code,
                IsActive = true
            };

            await _locationRepository.AddAsync(location);
            await _locationRepository.SaveChangesAsync();

            return new WarehouseLocationDto
            {
                Id = location.Id,
                WarehouseId = location.WarehouseId,
                Name = location.Name,
                Code = location.Code,
                IsActive = location.IsActive
            };
        }
    }
}