using ShopLite.Interfaces;
using ShopLite.Entities;
using ShopLite.Data;
using Microsoft.EntityFrameworkCore;

namespace ShopLite.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetNonDeletedProductsAsync()
        {
            return await _context
                            .Products
                            .Include(p => p.Category)
                            .IgnoreQueryFilters()
                            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context
                            .Products
                            .Include(p => p.Category)
                            .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteProductAsync(Product product)
        {
            product.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}