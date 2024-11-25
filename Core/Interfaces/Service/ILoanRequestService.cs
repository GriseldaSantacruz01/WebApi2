using Core.DTOs.LoanRequest;
using Core.Entities;

namespace Core.Interfaces.Service
{
    public interface ILoanRequestService
    {
        Task<Response> VerifyMonths(int months);
        Task<Response> VerifyCustomer(int customerId);
        Task<Response> VerifyId(int loanId);
        Task<string> CreateLoanRequest(CreateLoanRequest createLoanRequest, int customerId);
        Task<string> HandleApprovalOrRejectionAsync(RequestResponse request);
    }
}
