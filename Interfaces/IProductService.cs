using ShopLite.DTOs;
using ShopLite.Entities;

namespace ShopLite.Interfaces
{

    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetProductByIdAsync(int id);
        Task<ProductDTO> AddProductAsync(CreateProductDTO cProductDTO);
        Task<ProductDTO> UpdateProductAsync(UpdateProductDTO uProductDTO);
        Task DeleteProductAsync(int id);

        Task<bool> IsProductAvailable(int productId, int requiredQuantity);
        Task<bool> IsProductNameUnique(string productName);
    }
}