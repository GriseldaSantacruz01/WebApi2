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

        public InstallmentRepository(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task UpdateInstallments(List<Installment> installments)
        {
            _context.Installments.UpdateRange(installments);
            await _context.SaveChangesAsync();
        }
        public async Task AddInstallment(Installment installment)
        {
            await _context.Installments.AddAsync(installment);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Installment>> GetInstallmentsByApprovedLoanId(int loanId)
        {
            return await _context.Installments
                .Where(i => i.ApprovedLoanId == loanId)
                .OrderBy(i => i.DueDate) 
                .ToListAsync();
        }
        public async Task<List<Installment>> GetDelayedInstallmentsWithLoanAndCustomer()
        {
            return await _context.Installments
                .Where(i => i.DueDate < DateTime.UtcNow && !i.PaymentDate.HasValue)
                .Include(i => i.ApprovedLoan)
                .ThenInclude(al => al.Customer)
                .ToListAsync();
        }
    }
}
