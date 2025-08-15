using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLite.DTOs
{
    public class CreateCategoryDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; init; } = null!;

        [MaxLength(200)]
        public string? Description { get; init; }
        
    }
}