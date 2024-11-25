using Core.DTOs.ApprovedLoan;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using Mapster;

namespace Infrastructure.Service
{
    public class ApprovedLoanService : IApprovedLoanService
    {
        private readonly IApprovedLoanRepository _approvedLoanRepository;
        private readonly IInstallmentRepository _installmentRepository;
        private readonly ILoanRequestRepository _loanRequestRepository;
        private readonly ICustomerRepository _customerRepository;
        public ApprovedLoanService (IApprovedLoanRepository approvedLoanRepository, IInstallmentRepository installmentRepository, ILoanRequestRepository loanRequestRepository, ICustomerRepository customerRepository)
        {
            _approvedLoanRepository = approvedLoanRepository;
            _installmentRepository = installmentRepository;
            _loanRequestRepository = loanRequestRepository;
            _customerRepository = customerRepository;
        }
        
        public async Task<LoanDetailsResponse> GetLoanById(ApprovedLoan loan)
        {
            var approved = await _approvedLoanRepository.GetLoanById(loan.ApprovedLoanId);
            var customer = await _customerRepository.GetById(loan.CustomerId);
            var installment = await _installmentRepository.GetInstallment(approved.ApprovedLoanId);

            var response = approved.Adapt<LoanDetailsResponse>();
            response.Customer = customer;
            response.Installment = installment;
            return response;

        }
    }
}
