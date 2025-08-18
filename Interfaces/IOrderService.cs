using ShopLite.DTOs;
using ShopLite.Entities;

namespace ShopLite.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO?> GetOrderByIdAsync(int id);
        Task AddOrderAsync(CreateOrderDTO cOrderDTO);
        Task UpdateOrderAsync(UpdateOrderDTO uOrderDTO);
        Task DeleteOrderAsync(int id);

        Task<bool> AddProductToOrderAsync(int orderId, CreateOrderItemDTO orderItemDTO);
        Task<bool> RemoveProductFromOrderAsync(int productId, int orderId);
        Task<decimal> CalculateOrderTotalAsync(int orderId);
    }
}