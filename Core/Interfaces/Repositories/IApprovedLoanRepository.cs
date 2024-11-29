using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IApprovedLoanRepository
    {
        Task AddApprovedLoan(ApprovedLoan approvedLoan);
        Task<ApprovedLoan> GetLoanById (int id);
        Task UpdateApprovedLoan(ApprovedLoan approvedLoan);
    }
}
