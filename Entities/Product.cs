using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShopLite.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(200)]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [JsonPropertyName("category_id")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}