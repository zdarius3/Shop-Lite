using ShopLite.DTOs;
using ShopLite.Interfaces;
using ShopLite.Entities;
using ShopLite.Repositories;

namespace ShopLite.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerService(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _customerRepo.GetAllCustomersAsync();
            return customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                FullName = c.FullName,
                Address = c.Address,
                Email = c.Email
            });
        }

        public async Task<CustomerDTO?> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepo.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }

            return new CustomerDTO
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Address = customer.Address,
                Email = customer.Email
            };
        }

        public async Task<CustomerDTO> AddCustomerAsync(CreateCustomerDTO cCustomerDTO)
        {
            var customer = new Customer
            {
                FullName = cCustomerDTO.FullName,
                Address = cCustomerDTO.Address,
                Email = cCustomerDTO.Email
            };

            await _customerRepo.AddCustomerAsync(customer);
            return new CustomerDTO
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Address = customer.Address,
                Email = customer.Email
            };
        }

        public async Task<CustomerDTO> UpdateCustomerAsync(UpdateCustomerDTO uCustomerDTO)
        {
            var customer = await _customerRepo.GetCustomerByIdAsync(uCustomerDTO.Id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {uCustomerDTO.Id} not found.");
            }

            if (uCustomerDTO.FullName != null)
            {
                customer.FullName = uCustomerDTO.FullName;
            }
            if (uCustomerDTO.Address != null)
            {
                customer.Address = uCustomerDTO.Address;
            }
            if (uCustomerDTO.Email != null)
            {
                customer.Email = uCustomerDTO.Email;
            }

            await _customerRepo.UpdateCustomerAsync(customer);
            return new CustomerDTO
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Address = customer.Address,
                Email = customer.Email
            };
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _customerRepo.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }

            await _customerRepo.DeleteCustomerAsync(customer);
        }

        public async Task<CustomerDTO> GetCustomerByEmailAsync(string email)
        {
            var customer = await _customerRepo.GetCustomerByEmailAsync(email);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with email {email} not found.");
            }

            return new CustomerDTO
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Address = customer.Address,
                Email = customer.Email
            };
        }

        public async Task<IEnumerable<OrderDTO>> GetCustomerOrdersByIdAsync(int id)
        {
            var orders = await _customerRepo.GetCustomerOrdersByIdAsync(id);
            return orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer.FullName,
                OrderDate = o.OrderDate,
                Status = o.Status.ToString(),
                OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList(),
                TotalAmount = CalculateTotalAmount(new List<Order> { o })
            }).ToList();
        }

        public async Task<decimal> GetCustomerTotalSpentAsync(int id)
        {
            return CalculateTotalAmount(await _customerRepo.GetCustomerOrdersByIdAsync(id));
        }

        private decimal CalculateTotalAmount(IEnumerable<Order> orders)
        {
            return orders.Sum(o => o.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice));
        }
    }
}