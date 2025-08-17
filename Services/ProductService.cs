using ShopLite.DTOs;
using ShopLite.Interfaces;
using ShopLite.Entities;

namespace ShopLite.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId
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
                Price = product.Price,
                CategoryId = product.CategoryId
            };
        }

        public async Task AddProductAsync(CreateProductDTO cProductDTO)
        {
            if (!IsProductNameUnique(cProductDTO.Name))
            {
                throw new InvalidOperationException($"Product name '{cProductDTO.Name}' already exists.");
            }

            var product = new Product
            {
                Name = cProductDTO.Name,
                Description = cProductDTO.Description,
                Stock = cProductDTO.Stock,
                Price = cProductDTO.Price,
                CategoryId = cProductDTO.CategoryId

            };

            await _productRepository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(UpdateProductDTO uProductDTO)
        {
            var product = await _productRepository.GetProductByIdAsync(uProductDTO.Id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {uProductDTO.Id} not found.");
            }

            if (uProductDTO.Name != null && !IsProductNameUnique(uProductDTO.Name))
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

            if (uProductDTO.CategoryId.HasValue)
            {
                product.CategoryId = uProductDTO.CategoryId.Value;
                //should update the category name as well if needed
                //product.Category.Name = uProductDTO.CategoryName -> could be this approach
                //or maybe have a static method in CategoryService to update the name
            }

            await _productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            await _productRepository.DeleteProductAsync(id);
        }


        public bool IsProductAvailable(int productId, int requiredQuantity)
        {
            var product = _productRepository.GetProductByIdAsync(productId).Result;
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

        public bool IsProductNameUnique(string productName)
        {
            var products = _productRepository.GetAllProductsAsync().Result;
            return !products.Any(p =>
                p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
        }
    }
}


