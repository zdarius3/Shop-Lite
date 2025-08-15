using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLite.DTOs
{
    public class CreateCustomerDTO
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; init; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; init; } = null!;

        [Required]
        [MaxLength(200)]
        public string Address { get; init; } = null!;
    }
}