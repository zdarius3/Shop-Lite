using ShopLite.DTOs;
using ShopLite.Entities;

namespace ShopLite.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO?> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(CreateCategoryDTO cCategoryDTO);
        Task UpdateCategoryAsync(UpdateCategoryDTO uCategoryDTO);
        Task DeleteCategoryAsync(int id);

        bool IsCategoryNameUnique(string categoryName);
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);

    }
}