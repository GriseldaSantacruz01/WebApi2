using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IInstallmentRepository
    {
        Task UpdateAsync(List<Installment> installments);
        Task<List<Installment>> GetInstallments(int loanId);
        Task AddAsync(Installment installment);
        Task<List<Installment>> GetDelayedInstallmentsWithLoanAndCustomer(int approvedLoanId);    
    }
}
