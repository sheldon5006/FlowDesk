using System.ComponentModel.DataAnnotations;

namespace FlowDesk.Api.DTOs
{
    public class WarehouseLocationDto
    {
        public int Id { get; set; }

        public int WarehouseId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }

    public class CreateWarehouseLocationDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Code { get; set; } = string.Empty;
    }
}
