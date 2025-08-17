using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLite.DTOs
{
    public class ProductDTO
    {
        public int Id { get; init; }

        public string Name { get; init; } = null!;

        public string? Description { get; init; }
        
        public int Stock { get; init; }

        public decimal Price { get; init; }

        public int CategoryId { get; init; }

        public string CategoryName { get; init; } = null!;
    }
}