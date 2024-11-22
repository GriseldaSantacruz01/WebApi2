using Core.Interfaces.Repositories;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class ApprovedLoanRepository : IApprovedLoan
{
    private readonly AplicationDbContext _context;

    public ApprovedLoanRepository(AplicationDbContext context)
    {
        _context = context;
    }
}
