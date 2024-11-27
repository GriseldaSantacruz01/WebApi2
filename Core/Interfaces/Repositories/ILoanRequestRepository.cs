using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface ILoanRequestRepository
    {
        Task AddAsync(LoanRequest loanRequest);
        Task<LoanRequest> GetByIdAsync(int id);
        Task UpdateAsync(LoanRequest loanRequest);
        Task<LoanRequest> VerifyId(int loanId);
        Task<TermIR> VerifyMonths(int months);
        


    }
}
