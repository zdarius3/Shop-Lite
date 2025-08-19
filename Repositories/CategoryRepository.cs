using ShopLite.Interfaces;
using ShopLite.Entities;
using ShopLite.Data;
using Microsoft.EntityFrameworkCore;

namespace ShopLite.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopContext _context;

        public CategoryRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetCategoryNameByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category?.Name ?? "Unknown";
        }

        public async Task<bool> AddProductToCategoryAsync(int catId, Product product)
        {
            var category = await _context.Categories.FindAsync(catId);
            if (category == null)
            {
                return false;
            }
            category.Products.Add(product);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProductFromCategoryAsync(int catId, Product product)
        {
            var category = await _context.Categories.FindAsync(catId);
            if (category == null)
            {
                return false;
            }
            category.Products.Remove(product);
            await _context.SaveChangesAsync();
            
            return true;
        }
    }
}