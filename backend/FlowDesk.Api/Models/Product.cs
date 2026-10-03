namespace FlowDesk.Api.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Sku { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal ReorderThreshold { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
    }
}