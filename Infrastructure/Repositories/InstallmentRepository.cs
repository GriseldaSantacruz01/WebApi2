    using Core.DTOs.Installments;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class InstallmentRepository : IInstallmentRepository
    {
        private readonly AplicationDbContext _context;
        private readonly IGeneralService _termService;
        

        public InstallmentRepository(AplicationDbContext context, IGeneralService termService)
        {
            _context = context;
            _termService = termService;
        }


       

        public async Task UpdateAsync(List<Installment> installments)
        {
            _context.Installments.UpdateRange(installments);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Installment installment)
        {
            await _context.Installments.AddAsync(installment);
            await _context.SaveChangesAsync();
        }

        public async Task<TermIR> VerifyMonths (int months)
        {
            var entity = await _context.TermIRs.FirstOrDefaultAsync(x => x.Months == months);
            return entity!;
        }

        public async Task<List<Installment>> GetInstallments(int loanId)
        {
            return await _context.Installments
                .Where(i => i.ApprovedLoanId == loanId)
                .OrderBy(i => i.DueDate) 
                .ToListAsync();
        }
        public async Task<List<Installment>> GetByStatus(string status)
        {
            return await _context.Installments
                .Where(i => i.InstallmentStatus == status)
                .OrderBy(i => i.DueDate)
                .ToListAsync();
        }

        public async Task<List<Installment>> GetDelayedInstallmentsWithLoanAndCustomer(int approvedLoanId)
        {
            return await _context.Installments
                .Where(i => i.ApprovedLoanId == approvedLoanId && i.DueDate < DateTime.UtcNow && !i.PaymentDate.HasValue)
                .Include(i => i.ApprovedLoan)
                .ThenInclude(al => al.Customer)
                .ToListAsync();
        }


    }
}
