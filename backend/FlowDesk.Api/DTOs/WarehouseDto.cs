using System.ComponentModel.DataAnnotations;

namespace FlowDesk.Api.DTOs
{
        public class WarehouseDto
        {
            public int Id { get; set; }

            public string Name { get; set; } = string.Empty;

            public string Code { get; set; } = string.Empty;

            public string Address { get; set; } = string.Empty;

            public bool IsActive { get; set; }
        }

        public class CreateWarehouseDto
        {
            [Required]
            [MaxLength(100)]
            public string Name { get; set; } = string.Empty;

            [Required]
            [MaxLength(20)]
            public string Code { get; set; } = string.Empty;

            [Required]
            [MaxLength(250)]
            public string Address { get; set; } = string.Empty;
        }
    
}
