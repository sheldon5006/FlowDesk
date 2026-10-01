using FlowDesk.Api.DTOs;
using FlowDesk.Api.Models;
using FlowDesk.Api.Repositories;
using FlowDesk.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FlowDesk.Api.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IRepository<Warehouse> _warehouseRepository;

        public WarehouseService(
            IRepository<Warehouse> warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<List<WarehouseDto>> GetAllAsync()
        {
            return await _warehouseRepository
                .Query()
                .AsNoTracking()
                .Select(x => new WarehouseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Address = x.Address,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<WarehouseDto?> GetByIdAsync(int id)
        {
            return await _warehouseRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new WarehouseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Address = x.Address,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<WarehouseDto> CreateAsync(
            CreateWarehouseDto dto)
        {
            var code = dto.Code.Trim().ToUpperInvariant();

            var existingWarehouse = await _warehouseRepository
                .Query()
                .AnyAsync(x => x.Code == code);

            if (existingWarehouse)
            {
                throw new InvalidOperationException(
                    "A warehouse with this code already exists.");
            }

            var warehouse = new Warehouse
            {
                Name = dto.Name.Trim(),
                Code = code,
                Address = dto.Address.Trim(),
                IsActive = true
            };

            await _warehouseRepository.AddAsync(warehouse);
            await _warehouseRepository.SaveChangesAsync();

            return new WarehouseDto
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Code = warehouse.Code,
                Address = warehouse.Address,
                IsActive = warehouse.IsActive
            };
        }
    }
}