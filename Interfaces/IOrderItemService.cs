using ShopLite.DTOs;
using ShopLite.Entities;

namespace ShopLite.Interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDTO>> GetAllOrderItemsAsync();
        Task<OrderItemDTO?> GetOrderItemByIdAsync(int id);
        Task<OrderItemDTO> AddOrderItemAsync(CreateOrderItemDTO cOrderItemDTO);

        //can't update an OrderItem after its created
        Task DeleteOrderItemAsync(int id);

        Task<decimal> CalculateTotalAsync(int OrderItemId);
    }
}