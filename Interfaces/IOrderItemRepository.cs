using ShopLite.Entities;

namespace ShopLite.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
        Task<OrderItem?> GetOrderItemByIdAsync(int id);
    }
}