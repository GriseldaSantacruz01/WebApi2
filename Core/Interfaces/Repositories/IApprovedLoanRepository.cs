using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IApprovedLoanRepository
    {
        Task AddAsync(ApprovedLoan approvedLoan);
        Task<ApprovedLoan> GetLoanById (int id);
        Task UpdateAsync(int loanId);
    }
}
