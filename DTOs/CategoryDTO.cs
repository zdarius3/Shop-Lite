using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLite.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; init; }

        public string Name { get; init; } = null!;
        
        public List<ProductDTO> Products { get; init; } = new List<ProductDTO>();
    }
}