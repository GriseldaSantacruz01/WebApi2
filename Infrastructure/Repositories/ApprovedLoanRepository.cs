using Core.Interfaces.Repositories;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class ApprovedLoanRepository : IApprovedLoan
{
    private readonly ApplicationDbContext _context;

    public ApprovedLoanRepository(ApplicationDbContext context)
    {
        _context = context;
    }
}
