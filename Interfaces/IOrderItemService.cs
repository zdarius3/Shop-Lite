using ShopLite.DTOs;
using ShopLite.Entities;

namespace ShopLite.Interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDTO>> GetAllOrderItemsAsync();
        Task<OrderItemDTO?> GetOrderItemByIdAsync(int id);
    }
}