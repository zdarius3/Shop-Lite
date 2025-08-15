using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLite.DTOs
{
    public class CreateOrderItemDTO
    {

        [Required]
        public int ProductId { get; init; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; init; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}