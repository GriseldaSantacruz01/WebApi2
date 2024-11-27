using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;

namespace Infrastructure.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetById(id);
            return customer;
        }
    }
}
