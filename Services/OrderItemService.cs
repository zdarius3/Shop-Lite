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
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice
            };
        }

        public async Task AddOrderItemAsync(CreateOrderItemDTO cOrderItemDTO)
        {
            var order = await _orderRepo.GetOrderByIdAsync(cOrderItemDTO.OrderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {cOrderItemDTO.OrderId} not found.");
            }

            var orderItem = new OrderItem
            {
                ProductId = cOrderItemDTO.ProductId,
                Quantity = cOrderItemDTO.Quantity,
                UnitPrice = cOrderItemDTO.UnitPrice
            };

            await _orderItemRepo.AddOrderItemAsync(orderItem);
            order.OrderItems.Add(orderItem);
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var orderItem = await _orderItemRepo.GetOrderItemByIdAsync(id);
            if (orderItem == null)
            {
                throw new KeyNotFoundException($"OrderItem with ID {id} not found.");
            }

            var order = await _orderRepo.GetOrderByIdAsync(orderItem.OrderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderItem.OrderId} not found.");
            }

            await _orderItemRepo.DeleteOrderItemAsync(orderItem);
            order.OrderItems.Remove(orderItem);
        }


        public async Task<decimal> CalculateTotalAsync(int orderItemId)
        {
            var orderItem = await _orderItemRepo.GetOrderItemByIdAsync(orderItemId);
            if (orderItem == null)
            {
                throw new KeyNotFoundException($"OrderItem with ID {orderItemId} not found.");
            }
            return orderItem.Quantity * orderItem.UnitPrice;
        }
    }
}