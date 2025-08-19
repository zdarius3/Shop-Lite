using ShopLite.Interfaces;
using ShopLite.Entities;
using ShopLite.Data;
using Microsoft.EntityFrameworkCore;

namespace ShopLite.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ShopContext _context;

        public OrderItemRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
            return await _context.OrderItems
                                    .Include(oi => oi.Product)
                                    .ToListAsync();
        }

        public async Task<OrderItem?> GetOrderItemByIdAsync(int id)
        {
            return await _context.OrderItems
                                    .Include(oi => oi.Product)
                                    .FirstOrDefaultAsync(oi => oi.Id == id);
        }

        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderItemAsync(OrderItem orderItem)
        {
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}