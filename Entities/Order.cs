using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShopLite.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public Status status { get; set; } = Status.Pending;

        public ICollection<OrderItem>? OrderItems { get; set; }

    }

    public enum Status
    {
        Pending,
        Shipped,
        Delivered,
        Cancelled
    }
}