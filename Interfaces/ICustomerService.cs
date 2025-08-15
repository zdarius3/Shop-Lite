using ShopLite.Entities;

namespace ShopLite.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);


        Task<Customer> GetCustomerByEmailAsync(string email);

        Task<IEnumerable<Customer>> GetCustomerCustomersIdAsync(int customerId);
        
        Task<decimal> GetCustomerTotalSpentAsync(int customerId);

    }
}