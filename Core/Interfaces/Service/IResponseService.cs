using Core.Entities;

namespace Core.Interfaces.Service
{
    public interface IResponseService
    {
        Task<Response> VerifyLoanApprovedId(int loanId);
        Task<Response> VerifyMonths(int months);
        Task<Response> VerifyCustomer(int customer);
        Task<Response> VerifyLoanId(int loanId);
    }
}
