using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AplicationDbContext _context;
        public CustomerRepository(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Customer> GetCustomerById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            return customer!;
        } 
    }
}
