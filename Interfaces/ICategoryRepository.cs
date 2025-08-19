using ShopLite.Entities;

namespace ShopLite.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        Task<string> GetCategoryNameByIdAsync(int id);

        Task<bool> AddProductToCategoryAsync(int categoryId, Product product);
        Task<bool> DeleteProductFromCategoryAsync(int categoryId, Product product);
    }
}