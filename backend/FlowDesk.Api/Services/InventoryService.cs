using FlowDesk.Api.DTOs;
using FlowDesk.Api.Models;
using FlowDesk.Api.Repositories;
using FlowDesk.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FlowDesk.Api.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IRepository<Inventory> _inventoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<WarehouseLocation> _locationRepository;

        public InventoryService(
            IRepository<Inventory> inventoryRepository,
            IRepository<Product> productRepository,
            IRepository<WarehouseLocation> locationRepository)
        {
            _inventoryRepository = inventoryRepository;
            _productRepository = productRepository;
            _locationRepository = locationRepository;
        }

        public async Task<List<InventoryDto>> GetByWarehouseAsync(
            int warehouseId)
        {
            return await _inventoryRepository
                .Query()
                .AsNoTracking()
                .Where(x =>
                    x.WarehouseLocation.WarehouseId == warehouseId)
                .Select(x => new InventoryDto
                {
                    Id = x.Id,

                    ProductId = x.ProductId,

                    ProductSku = x.Product.Sku,

                    ProductName = x.Product.Name,

                    WarehouseLocationId = x.WarehouseLocationId,

                    LocationCode = x.WarehouseLocation.Code,

                    Quantity = x.Quantity
                })
                .ToListAsync();
        }

        public async Task<InventoryDto> CreateAsync(
            int warehouseId,
            CreateInventoryDto dto)
        {
            var product = await _productRepository
                .GetByIdAsync(dto.ProductId);

            if (product == null)
            {
                throw new KeyNotFoundException(
                    "Product not found.");
            }

            var location = await _locationRepository
                .Query()
                .FirstOrDefaultAsync(x =>
                    x.Id == dto.WarehouseLocationId &&
                    x.WarehouseId == warehouseId);

            if (location == null)
            {
                throw new KeyNotFoundException(
                    "Warehouse location not found.");
            }

            if (!location.IsActive)
            {
                throw new InvalidOperationException(
                    "Inventory cannot be added to an inactive location.");
            }

            var existingInventory = await _inventoryRepository
                .Query()
                .AnyAsync(x =>
                    x.ProductId == dto.ProductId &&
                    x.WarehouseLocationId == dto.WarehouseLocationId);

            if (existingInventory)
            {
                throw new InvalidOperationException(
                    "Inventory already exists for this product and location.");
            }

            var inventory = new Inventory
            {
                ProductId = dto.ProductId,
                WarehouseLocationId = dto.WarehouseLocationId,
                Quantity = dto.Quantity
            };

            await _inventoryRepository.AddAsync(inventory);
            await _inventoryRepository.SaveChangesAsync();

            return await GetByIdAsync(inventory.Id);
        }

        private async Task<InventoryDto> GetByIdAsync(int id)
        {
            return await _inventoryRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new InventoryDto
                {
                    Id = x.Id,

                    ProductId = x.ProductId,

                    ProductSku = x.Product.Sku,

                    ProductName = x.Product.Name,

                    WarehouseLocationId = x.WarehouseLocationId,

                    LocationCode = x.WarehouseLocation.Code,

                    Quantity = x.Quantity
                })
                .FirstAsync();
        }
    }
}