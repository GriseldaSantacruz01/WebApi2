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
        private readonly IGeneralService _generalService;
        public ApprovedLoanService (IApprovedLoanRepository approvedLoanRepository, 
                IInstallmentRepository installmentRepository, 
                ILoanRequestRepository loanRequestRepository, 
                ICustomerRepository customerRepository,
                IGeneralService generalService)
        {
            _approvedLoanRepository = approvedLoanRepository;
            _installmentRepository = installmentRepository;
            _loanRequestRepository = loanRequestRepository;
            _customerRepository = customerRepository;
            _generalService = generalService;
        }

        
        
        public async Task<LoanDetailsResponse> GetLoanById(int loanApprovedId)
        {
            var approved = await _approvedLoanRepository.GetLoanById(loanApprovedId);
            var installments = await _installmentRepository.GetInstallments(loanApprovedId);
            var customer = await _customerRepository.GetById(approved.CustomerId);
            

            var paidInstallments = installments.Count(i => i.PaymentDate.HasValue);
            var pendingInstallments = installments.Count - paidInstallments;

            
            var nextInstallment = installments.FirstOrDefault(i => !i.PaymentDate.HasValue);
            string nextDueDateMessage = nextInstallment != null
                ? nextInstallment.DueDate.ToString("yyyy-MM-dd")
                : "Todas las cuotas estan pagadas";


            var response = approved.Adapt<LoanDetailsResponse>();
            response.CustomerName = $"{customer.FirstName} {customer.LastName}";
            response.TotalAmount = Math.Round((decimal)_generalService
                .CalculateInstallmentAmount(approved.InterestRate, approved.Amount, approved.Months) * approved.Months);
            response.PaidInstallments = paidInstallments;
            response.Profit = Math.Round(response.TotalAmount - response.Amount) ;
            response.PendingInstallments = pendingInstallments;
            response.NextDueDate = nextDueDateMessage;
            return response;

        }

        
    }
}
