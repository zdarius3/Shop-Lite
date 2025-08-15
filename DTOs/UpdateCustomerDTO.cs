using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLite.DTOs
{
    public class UpdateCustomerDTO
    {
        [Required]
        public int Id { get; init; }

        [MaxLength(100)]
        public string? FullName { get; init; } = null!;

        [EmailAddress]
        public string? Email { get; init; } = null!;
        [MaxLength(200)]
        public string? Address { get; init; } = null!;
    }
}