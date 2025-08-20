using ShopLite.DTOs;
using ShopLite.Interfaces;
using ShopLite.Entities;
using ShopLite.Repositories;

namespace ShopLite.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepo.GetAllCategoriesAsync();
            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products?
                    .Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Stock = p.Stock,
                        Price = p.Price,
                        CategoryId = p.CategoryId,
                        CategoryName = c.Name,
                        IsDeleted = p.IsDeleted
                    }).ToList() ?? new List<ProductDTO>()
            });
        }

        public async Task<CategoryDTO?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products?
                    .Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        CategoryId = p.CategoryId,
                        CategoryName = category.Name,
                        IsDeleted = p.IsDeleted
                    }).ToList() ?? new List<ProductDTO>()
            };
        }

        public async Task<CategoryDTO> AddCategoryAsync(CreateCategoryDTO cCategoryDTO)
        {
            if (await IsCategoryNameUnique(cCategoryDTO.Name) == false)
            {
                throw new InvalidOperationException($"Category name '{cCategoryDTO.Name}' already exists.");
            }

            var category = new Category
            {
                Name = cCategoryDTO.Name
            };

            await _categoryRepo.AddCategoryAsync(category);
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Products = new List<ProductDTO>()
            };
        }

        public async Task<CategoryDTO> UpdateCategoryAsync(UpdateCategoryDTO uCategoryDTO)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(uCategoryDTO.Id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {uCategoryDTO.Id} not found.");
            }

            if (uCategoryDTO.Name != null && (await IsCategoryNameUnique(uCategoryDTO.Name) == false))
            {
                throw new InvalidOperationException($"Category name '{uCategoryDTO.Name}' already exists.");
            }

            category.Name = uCategoryDTO.Name ?? category.Name;

            await _categoryRepo.UpdateCategoryAsync(category);
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products?
                    .Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        CategoryId = p.CategoryId,
                        CategoryName = category.Name,
                        IsDeleted = p.IsDeleted
                    }).ToList() ?? new List<ProductDTO>()
            };
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            if (category.Products.Any())
            {
                throw new InvalidOperationException("Cannot delete category that still has products.");
            }

            await _categoryRepo.DeleteCategoryAsync(category);
        }

        public async Task<bool> IsCategoryNameUnique(string categoryName)
        {
            var categories = await _categoryRepo.GetAllCategoriesAsync();
            return !categories.Any(c =>
                c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
            }

            return category.Products?.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = category.Name,
                IsDeleted = p.IsDeleted
            }) ?? new List<ProductDTO>();
        }
        
    
    }
}