using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLite.DTOs
{
    public class CreateOrderDTO
    {
        [Required]
        public int CustomerId { get; init; }

        public List<CreateOrderItemDTO> OrderItems = new List<CreateOrderItemDTO>();

    }
}