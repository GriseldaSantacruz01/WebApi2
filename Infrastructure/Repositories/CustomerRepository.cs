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
    }
}
