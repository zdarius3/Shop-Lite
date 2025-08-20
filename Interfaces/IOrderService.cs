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

        Task<OrderDTO> AddOrderItemToOrderAsync(int orderId, CreateOrderItemDTO orderItemDTO);
        Task<OrderDTO> DeleteOrderItemFromOrderAsync(int orderId, int orderItemId);

        Task<decimal> CalculateOrderTotalAsync(int orderId);
    }
}