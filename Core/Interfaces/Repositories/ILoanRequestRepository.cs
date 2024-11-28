using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface ILoanRequestRepository
    {
        Task AddLoanRequest(LoanRequest loanRequest);
        Task<LoanRequest> GetLoanRequestById(int id);
        Task UpdateLoanRequestById(LoanRequest loanRequest);
        Task<TermIR> GetByMonths(int months);
    }
}
