using ShopLite.DTOs;
using ShopLite.Interfaces;
using ShopLite.Entities;
using ShopLite.Repositories;

namespace ShopLite.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerService _customerService;

        public OrderService(IOrderRepository orderRepository, ICustomerService customerService)
        {
            _orderRepository = orderRepository;
            _customerService = customerService;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                OrderDate = o.OrderDate,
                Status = o.Status.ToString(),
                OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList(),
            });
        }

        public async Task<OrderDTO?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            return new OrderDTO
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList(),
            };
        }

        public async Task<OrderDTO> AddOrderAsync(CreateOrderDTO cOrderDTO)
        {
            var customer = await _customerService.GetCustomerByIdAsync(cOrderDTO.CustomerId);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {cOrderDTO.CustomerId} not found.");
            }

            var order = new Order
            {
                CustomerId = cOrderDTO.CustomerId,
                OrderDate = DateTime.UtcNow,
                Status = Status.Pending,
                OrderItems = cOrderDTO.OrderItems.Select(oi => new OrderItem
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };

            await _orderRepository.AddOrderAsync(order);

            return new OrderDTO
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList(),
            };
        }

        public async Task<OrderDTO> UpdateOrderAsync(UpdateOrderDTO uOrderDTO)
        {
            var order = await _orderRepository.GetOrderByIdAsync(uOrderDTO.Id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {uOrderDTO.Id} not found.");
            }

            if (uOrderDTO.Status != null)
            {
                order.Status = Enum.Parse<Status>(uOrderDTO.Status, true);
            }

            await _orderRepository.UpdateOrderAsync(order);

            return new OrderDTO
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList(),
            };
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            await _orderRepository.DeleteOrderAsync(order);
        }

        public async Task<bool> AddOrderItemToOrderAsync(int orderId, CreateOrderItemDTO orderItemDTO)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }

            var orderItem = new OrderItem
            {
                OrderId = orderId,
                Order = order,
                ProductId = orderItemDTO.ProductId,
                Quantity = orderItemDTO.Quantity,
                UnitPrice = orderItemDTO.UnitPrice
            };

            order.OrderItems.Add(orderItem);
            await _orderRepository.UpdateOrderAsync(order);
            return true;
        }

        public async Task<bool> DeleteOrderItemFromOrderAsync(int orderId, int orderItemId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }

            var orderItem = order.OrderItems.FirstOrDefault(oi => oi.Id == orderItemId);
            if (orderItem == null)
            {
                throw new KeyNotFoundException($"OrderItem with ID {orderItemId} not found in Order with ID {orderId}.");
            }

            order.OrderItems.Remove(orderItem);
            await _orderRepository.UpdateOrderAsync(order);
            return true;
        }
        
        public async Task<decimal> CalculateOrderTotalAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }

            return order.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice);
        }
    }
}