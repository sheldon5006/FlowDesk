using System.ComponentModel.DataAnnotations;

namespace FlowDesk.Api.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Sku { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal ReorderThreshold { get; set; }

        public bool IsActive { get; set; }
    }

    public class CreateProductDto
    {
        [Required]
        [MaxLength(50)]
        public string Sku { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal ReorderThreshold { get; set; }
    }
}