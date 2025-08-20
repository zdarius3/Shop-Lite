using ShopLite.DTOs;
using ShopLite.Interfaces;
using ShopLite.Entities;
using ShopLite.Repositories;

namespace ShopLite.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepo;
        private readonly IOrderRepository _orderRepo;

        public OrderItemService(IOrderItemRepository orderItemRepo, IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
        }

        public async Task<IEnumerable<OrderItemDTO>> GetAllOrderItemsAsync()
        {
            var orderItems = await _orderItemRepo.GetAllOrderItemsAsync();
            return orderItems.Select(oi => new OrderItemDTO
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                ProductName = oi.Product.Name,
                OrderId = oi.OrderId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            });
        }

        public async Task<OrderItemDTO?> GetOrderItemByIdAsync(int id)
        {
            var orderItem = await _orderItemRepo.GetOrderItemByIdAsync(id);
            if (orderItem == null)
            {
                throw new KeyNotFoundException($"OrderItem with ID {id} not found.");
            }
            return new OrderItemDTO
            {
                Id = orderItem.Id,
                ProductId = orderItem.ProductId,
                OrderId = orderItem.OrderId,
                ProductName = orderItem.Product.Name,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice
            };
        }
    }
}