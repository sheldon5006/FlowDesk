namespace FlowDesk.Api.Models
{
    public class WarehouseLocation
    {
        public int Id { get; set; }

        public int WarehouseId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public Warehouse Warehouse { get; set; } = null!;
        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();

    }
}
