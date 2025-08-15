using ShopLite.Entities;

namespace ShopLite.Interfaces
{

    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);

        bool IsProductAvailable(int productId, int requiredQuantity);
        bool IsProductNameUnique(string productName);
    }
}