using Core.Interfaces.Repositories;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class LoanRequestRepository : ILoanRequestRepository
{
    private readonly AplicationDbContext _context;

    public LoanRequestRepository(AplicationDbContext context)
    {
        _context = context;
    }
}
