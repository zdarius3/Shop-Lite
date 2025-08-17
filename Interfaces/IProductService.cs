using ShopLite.DTOs;
using ShopLite.Entities;

namespace ShopLite.Interfaces
{

    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetProductByIdAsync(int id);
        Task AddProductAsync(CreateProductDTO cProductDTO);
        Task UpdateProductAsync(UpdateProductDTO uProductDTO);
        Task DeleteProductAsync(int id);

        bool IsProductAvailable(int productId, int requiredQuantity);
        bool IsProductNameUnique(string productName);
    }
}