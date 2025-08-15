using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShopLite.Entities;

namespace ShopLite.DTOs
{
    public class OrderDTO
    {
        public int Id { get; init; }

        public int CustomerId { get; init; }
        public string CustomerName { get; init; } = null!;

        public DateTime OrderDate { get; init; }
        public string Status { get; init; } = null!;

        public List<OrderItemDTO> OrderItems { get; init; } = new List<OrderItemDTO>();

        public decimal TotalAmount { get; init; }
    }
}