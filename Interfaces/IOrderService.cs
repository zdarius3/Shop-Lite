using ShopLite.DTOs;
using ShopLite.Entities;

namespace ShopLite.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO?> GetOrderByIdAsync(int id);
        Task<OrderDTO> AddOrderAsync(CreateOrderDTO cOrderDTO);
        Task<OrderDTO> UpdateOrderAsync(UpdateOrderDTO uOrderDTO);
        Task DeleteOrderAsync(int id);

        Task<bool> AddProductToOrderAsync(int orderId, CreateOrderItemDTO orderItemDTO);
        Task<bool> DeleteProductFromOrderAsync(int productId, int orderId);

        Task<bool> AddOrderItemToOrderAsync(int orderId, CreateOrderItemDTO orderItemDTO);
        Task<bool> DeleteOrderItemFromOrderAsync(int orderId, int orderItemId);

        Task<decimal> CalculateOrderTotalAsync(int orderId);
    }
}