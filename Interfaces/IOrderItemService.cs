using ShopLite.DTOs;
using ShopLite.Entities;

namespace ShopLite.Interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDTO>> GetAllOrderItemsAsync();
        Task<OrderItem?> GetOrderItemByIdAsync(int id);
        Task AddOrderItemAsync(CreateOrderItemDTO cOrderItemDTO);

        //can't update an OrderItem after its created
        Task DeleteOrderItemAsync(int id);

        Task<bool> ApplyDiscountToOrderItemAsync(int OrderItemId, decimal discountPercentage);
        Task<decimal> CalculateTotalAsync(int OrderItemId);
    }
}