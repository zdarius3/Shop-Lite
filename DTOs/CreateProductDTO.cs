using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLite.DTOs
{
    public class CreateProductDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; init; } = null!;

        [MaxLength(200)]
        public string? Description { get; init; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; init; }

        [Required]
        public int CategoryId { get; init; }
    }
}