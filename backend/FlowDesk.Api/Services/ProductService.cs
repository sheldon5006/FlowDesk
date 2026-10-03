using FlowDesk.Api.DTOs;
using FlowDesk.Api.Models;
using FlowDesk.Api.Repositories;
using FlowDesk.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FlowDesk.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(
            IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            return await _productRepository
                .Query()
                .AsNoTracking()
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Sku = x.Sku,
                    Name = x.Name,
                    Description = x.Description,
                    ReorderThreshold = x.ReorderThreshold,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            return await _productRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Sku = x.Sku,
                    Name = x.Name,
                    Description = x.Description,
                    ReorderThreshold = x.ReorderThreshold,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ProductDto> CreateAsync(
            CreateProductDto dto)
        {
            var sku = dto.Sku
                .Trim()
                .ToUpperInvariant();

            var existingProduct = await _productRepository
                .Query()
                .AnyAsync(x => x.Sku == sku);

            if (existingProduct)
            {
                throw new InvalidOperationException(
                    "A product with this SKU already exists.");
            }

            var product = new Product
            {
                Sku = sku,
                Name = dto.Name.Trim(),
                Description = dto.Description.Trim(),
                ReorderThreshold = dto.ReorderThreshold,
                IsActive = true
            };

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            return new ProductDto
            {
                Id = product.Id,
                Sku = product.Sku,
                Name = product.Name,
                Description = product.Description,
                ReorderThreshold = product.ReorderThreshold,
                IsActive = product.IsActive
            };
        }
    }
}