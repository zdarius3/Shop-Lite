using ShopLite.Entities;

namespace ShopLite.Interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
        Task<OrderItem?> GetOrderItemByIdAsync(int id);
        Task AddOrderItemAsync(OrderItem OrderItem);
        Task UpdateOrderItemAsync(OrderItem OrderItem);
        Task DeleteOrderItemAsync(int id);

        Task<bool> ApplyDiscountToOrderItemAsync(int OrderItemId, decimal discountPercentage);
        Task<decimal> CalculateTotalAsync(int OrderItemId);
    }
}