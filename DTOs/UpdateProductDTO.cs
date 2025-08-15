using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLite.DTOs
{
    public class UpdateProductDTO
    {
        [Required]
        public int Id { get; init; }

        [MaxLength(50)]
        public string? Name { get; init; }

        [MaxLength(200)]
        public string? Description { get; init; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal? Price { get; init; }

        public int? CategoryId { get; init; }
    }
}