using Core.DTOs.ApprovedLoan;
using Core.DTOs.LoanRequest;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ApprovedLoanRepository : IApprovedLoanRepository
{
    private readonly AplicationDbContext _context;
    public ApprovedLoanRepository(AplicationDbContext context)
    {
        _context = context;
    }
    public async Task AddApprovedLoan(ApprovedLoan approvedLoan)
    {
        await _context.ApprovedLoans.AddAsync(approvedLoan);
        await _context.SaveChangesAsync();
    }
    public async Task<ApprovedLoan> GetLoanById(int id)
    {
        var loan = await _context.ApprovedLoans.FirstOrDefaultAsync(x => x.ApprovedLoanId == id);
        return loan! ;
    }
    public async Task UpdateApprovedLoan(ApprovedLoan approvedLoan)
    {
        _context.ApprovedLoans.Update(approvedLoan);
        await _context.SaveChangesAsync();
    }

}
