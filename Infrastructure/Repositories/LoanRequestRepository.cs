using Core.Interfaces.Repositories;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class LoanRequestRepository : ILoanRequestRepository
{
    private readonly ApplicationDbContext _context;

    public LoanRequestRepository(ApplicationDbContext context)
    {
        _context = context;
    }
}
