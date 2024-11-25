using Core.Entities;

namespace Core.Interfaces.Service
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomer(int id);
    }
}
