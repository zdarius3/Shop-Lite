using ShopLite.Interfaces;
using ShopLite.Entities;
using ShopLite.Data;
using Microsoft.EntityFrameworkCore;

namespace ShopLite.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopContext _context;

        public OrderRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                            .Include(o => o.Customer)
                            .Include(o => o.OrderItems)
                                .ThenInclude(oi => oi.Product)
                            .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                            .Include(o => o.Customer)
                            .Include(o => o.OrderItems)
                                .ThenInclude(oi => oi.Product)
                            .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}