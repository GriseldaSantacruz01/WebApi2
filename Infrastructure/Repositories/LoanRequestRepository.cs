using Core.DTOs.LoanRequest;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LoanRequestRepository : ILoanRequestRepository
{
    private readonly AplicationDbContext _context;

    public LoanRequestRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> CreateLoanRequest(CreateLoanRequest createLoanRequest, int customerId)
    {
        
        var existingTerm = await _context.TermIRs
            .FirstOrDefaultAsync(x => x.Months == createLoanRequest.Months);
        var existingCustomer = await _context.Customers
            .FirstOrDefaultAsync(x => x.CustomerId == customerId);

        var entity = createLoanRequest.Adapt<LoanRequest>();
        entity.Term = existingTerm!;
        entity.Customer = existingCustomer!;

        _context.LoanRequests.Add(entity);
        await _context.SaveChangesAsync();

        return $"La solicitud de préstamo está siendo procesada. El Id de la solicitud es {entity.LoanId}";
    }

    public async Task<LoanRequest> GetByIdAsync(int id)
    {
        return await _context.LoanRequests.FindAsync(id);
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

    public async Task<Customer> VerifyCustomer(int customerId)
    {
        var result = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
        return result!;
    }

    public async Task<TermIR> VerifyMonths(int months)
    {
        var entity = await _context.TermIRs.FirstOrDefaultAsync(x => x.Months == months);
        return entity!;
    }
}
