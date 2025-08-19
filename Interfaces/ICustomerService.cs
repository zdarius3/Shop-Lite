using ShopLite.DTOs;
using ShopLite.Entities;

namespace ShopLite.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync();
        Task<CustomerDTO?> GetCustomerByIdAsync(int id);
        Task<CustomerDTO> AddCustomerAsync(CreateCustomerDTO cCustomerDTO);
        Task<CustomerDTO> UpdateCustomerAsync(UpdateCustomerDTO uCustomerDTO);
        Task DeleteCustomerAsync(int id);


        Task<CustomerDTO> GetCustomerByEmailAsync(string email);

        Task<IEnumerable<OrderDTO>> GetCustomerOrdersByIdAsync(int customerId);
        
        Task<decimal> GetCustomerTotalSpentAsync(int customerId);

    }
}