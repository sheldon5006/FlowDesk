using System.ComponentModel.DataAnnotations;

namespace FlowDesk.Api.DTOs
{
    public class InventoryDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductSku { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public int WarehouseLocationId { get; set; }

        public string LocationCode { get; set; } = string.Empty;

        public decimal Quantity { get; set; }
    }

    public class CreateInventoryDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int WarehouseLocationId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Quantity { get; set; }
    }

}
