using ShopLite.Entities;

namespace ShopLite.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);

        Task<bool> AddProductToOrderAsync(Product product, int quantity, int orderId);
        Task<bool> RemoveProductFromOrderAsync(int productId, int orderId);
        Task<decimal> CalculateOrderTotalAsync(int orderId);
    }
}