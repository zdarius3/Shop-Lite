using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLite.DTOs
{
    public class OrderItemDTO
    {
        public int Id { get; init; }

        public int ProductId { get; init; }
        public int OrderId { get; init; }

        public string ProductName { get; init; } = null!;

        public int Quantity { get; init; }

        public decimal UnitPrice { get; init; }
    }
}