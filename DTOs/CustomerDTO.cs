using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLite.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; init; }
        public string FullName { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string Address { get; init; } = null!;

    }
}