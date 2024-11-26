using Core.DTOs.Installments;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IInstallmentRepository
    {
        Task CreateInstallment(SimulateInstallment simulateInstallment);
        Task<TermIR> VerifyMonths(int months);
        Task UpdateAsync(IEnumerable<Installment> installments);
        Task<List<Installment>> GetInstallments(int loanId);
    }
}
