using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLite.DTOs
{
    public class UpdateOrderDTO
    {
        [Required]
        public int Id { get; init; }
        
        public string? Status { get; init; }
    }
}