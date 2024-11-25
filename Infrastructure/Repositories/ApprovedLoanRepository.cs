using Core.DTOs.ApprovedLoan;
using Core.DTOs.LoanRequest;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ApprovedLoanRepository : IApprovedLoanRepository
{
    private readonly AplicationDbContext _context;

    public ApprovedLoanRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ApprovedLoan approvedLoan)
    {
        await _context.ApprovedLoans.AddAsync(approvedLoan);
        await _context.SaveChangesAsync();
    }

    public async Task<ApprovedLoan> GetLoanById(int id)
    {
        var loan = await _context.ApprovedLoans.FindAsync(id);
        return loan! ;
    }

}
