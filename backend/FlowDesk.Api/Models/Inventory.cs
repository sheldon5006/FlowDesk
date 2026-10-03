namespace FlowDesk.Api.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int WarehouseLocationId { get; set; }

        public decimal Quantity { get; set; }

        public Product Product { get; set; } = null!;

        public WarehouseLocation WarehouseLocation { get; set; } = null!;
    }
}
