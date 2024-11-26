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

        public async Task CreateInstallment(SimulateInstallment simulateInstallment)
        {
            var entity = await _context.TermIRs.FirstOrDefaultAsync(x => x.Months == simulateInstallment.Months);

        }

        public async Task UpdateAsync(IEnumerable<Installment> installments)
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

        


    }
}
