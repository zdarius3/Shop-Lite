using ShopLite.Entities;

namespace ShopLite.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);

        bool IsCategoryNameUnique(string categoryName);
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);

    }
}