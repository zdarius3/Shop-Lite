using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLite.DTOs
{
    public class UpdateCategoryDTO
    {
        [Required]
        public int Id { get; init; }

        [MaxLength(50)]
        public string? Name { get; init; }

        [MaxLength(200)]
        public string? Description { get; init; }
    }
}