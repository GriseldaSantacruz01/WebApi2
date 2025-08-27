using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IInstallmentRepository
    {
        Task UpdateInstallments(List<Installment> installments);
        Task<List<Installment>> GetInstallmentsByApprovedLoanId(int loanId);
        Task AddInstallment(Installment installment);
        Task<List<Installment>> GetDelayedInstallmentsWithLoanAndCustomer();
    }
}
