using ShopLite.DTOs;
using ShopLite.Interfaces;
using ShopLite.Entities;

namespace ShopLite.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository,
                ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return (IEnumerable<ProductDTO>)products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            });
        }

        public async Task<IEnumerable<ProductDTO>> GetNonDeletedProductsAsync()
        {
            var products = await _productRepository.GetNonDeletedProductsAsync();
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            });
        }

        public async Task<ProductDTO?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
        }

        public async Task<ProductDTO> AddProductAsync(CreateProductDTO cProductDTO)
        {
            if (await IsProductNameUnique(cProductDTO.Name) == false)
            {
                throw new InvalidOperationException($"Product name '{cProductDTO.Name}' already exists.");
            }

            var category = await _categoryRepository.GetCategoryByIdAsync(cProductDTO.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {cProductDTO.CategoryId} not found.");
            }
            
            var product = new Product
            {
                Name = cProductDTO.Name,
                Description = cProductDTO.Description,
                Stock = cProductDTO.Stock,
                Price = cProductDTO.Price,
                CategoryId = cProductDTO.CategoryId,
                Category = category
            };

            await _categoryRepository.AddProductToCategoryAsync(product.CategoryId, product);
            //await _productRepository.AddProductAsync(product);

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Stock = product.Stock,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategoryName = category.Name
            };
        }

        public async Task<ProductDTO> UpdateProductAsync(UpdateProductDTO uProductDTO)
        {
            var product = await _productRepository.GetProductByIdAsync(uProductDTO.Id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {uProductDTO.Id} not found.");
            }

            if (uProductDTO.Name != null && (await IsProductNameUnique(uProductDTO.Name) == false))
            {
                throw new InvalidOperationException($"Product name '{uProductDTO.Name}' already exists.");
            }

            if (uProductDTO.Name != null)
            {
                product.Name = uProductDTO.Name;
            }

            if (uProductDTO.Description != null)
            {
                product.Description = uProductDTO.Description;
            }

            if (uProductDTO.Stock.HasValue)
            {
                product.Stock = uProductDTO.Stock.Value;
            }

            if (uProductDTO.Price.HasValue)
            {
                product.Price = uProductDTO.Price.Value;
            }

            string oldCategoryName = product.Category.Name;
            string newCategoryName = "";
            if (uProductDTO.CategoryId.HasValue)
            {
                //delete from the old category
                await _categoryRepository.DeleteProductFromCategoryAsync(product.CategoryId, product);
                var newCategory = await _categoryRepository.GetCategoryByIdAsync(uProductDTO.CategoryId.Value);
                if (newCategory == null)
                {
                    throw new KeyNotFoundException("Category with ID" +
                       $"{uProductDTO.CategoryId.Value} not found.");
                }

                //add to the new category
                product.CategoryId = uProductDTO.CategoryId.Value;
                product.Category = newCategory;
                newCategoryName = newCategory.Name;
                await _categoryRepository.AddProductToCategoryAsync(newCategory.Id, product);
            }

            await _productRepository.UpdateProductAsync(product);
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CategoryName = newCategoryName.Equals("") ? oldCategoryName : newCategoryName
            };
        }

        public async Task SoftDeleteProductAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            await _productRepository.SoftDeleteProductAsync(product);
        }


        public async Task<bool> IsProductAvailable(int productId, int requiredQuantity)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }

            if (product.Stock < requiredQuantity)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> IsProductNameUnique(string productName)
        {
            var products = await _productRepository.GetAllProductsAsync();
            return !products.Any(p =>
                p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
        }
    }
}


