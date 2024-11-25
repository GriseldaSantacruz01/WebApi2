using Core.DTOs.ApprovedLoan;
using Core.Entities;

namespace Core.Interfaces.Service
{
    public interface IApprovedLoanService
    {
        Task<LoanDetailsResponse> GetLoanById(ApprovedLoan loan);
    }
}
