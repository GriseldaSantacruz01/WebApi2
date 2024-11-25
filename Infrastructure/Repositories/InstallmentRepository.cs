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
        private readonly ITermService _termService;
        

        public InstallmentRepository(AplicationDbContext context, ITermService termService)
        {
            _context = context;
            _termService = termService;
        }

        public async Task<SimulateInstallmentResponse> CreateInstallment(SimulateInstallment simulateInstallment)
        {
            var entity = await _context.TermIRs.FirstOrDefaultAsync(x => x.Months == simulateInstallment.Months);

            var installment = new SimulateInstallmentResponse
            {
                InstallmentAmount = _termService.CalculateInstallmentAmount(entity!.InterestRate, simulateInstallment.Amount, simulateInstallment.Months),
                TotalAmount = _termService.CalculateInstallmentAmount(entity.InterestRate, simulateInstallment.Amount, simulateInstallment.Months) * simulateInstallment.Months,
            };

            return installment;
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

        public async Task<Installment> GetInstallment(int id)
        {
            var installment = await _context.Installments.FirstOrDefaultAsync(x => x.ApprovedLoanId == id);
            return installment!;
        }


    }
}
