using Core.DTOs.LoanRequest;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LoanRequestRepository : ILoanRequestRepository
{
    private readonly AplicationDbContext _context;

    public LoanRequestRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(LoanRequest loanRequest)
    {
      _context.LoanRequests.Add(loanRequest);
        await _context.SaveChangesAsync();
    }

    public async Task<LoanRequest> GetByIdAsync(int id)
    {
        var loan = await _context.LoanRequests.FindAsync(id);
        return loan!;
    }

    public async Task UpdateAsync(LoanRequest loanRequest)
    {
        _context.LoanRequests.Update(loanRequest);
        await _context.SaveChangesAsync();
    }

    public async Task<LoanRequest> VerifyId(int loanId)
    {
        var loanRequest = await _context.LoanRequests.FirstOrDefaultAsync(x => x.LoanId == loanId && x.RequestStatus == "Pendiente");
        return loanRequest!;
    }
    public async Task<TermIR> VerifyMonths(int months)
    {
        var entity = await _context.TermIRs.FirstOrDefaultAsync(x => x.Months == months);
        return entity!;
    }
}
