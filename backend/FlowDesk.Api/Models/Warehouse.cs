namespace FlowDesk.Api.Models
{
    public class Warehouse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public ICollection<WarehouseLocation> Locations { get; set; }
            = new List<WarehouseLocation>();
    }
}
