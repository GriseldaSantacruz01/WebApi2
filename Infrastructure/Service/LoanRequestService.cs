using Core.DTOs.LoanRequest;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using Infrastructure.Repositories;
using Mapster;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service;

public class LoanRequestService : ILoanRequestService
{
    private readonly ILoanRequestRepository _loanRequestRepository;
    private readonly IApprovedLoanRepository _approvedLoanRepository;
    private readonly IInstallmentRepository _installmentRepository;
    private readonly IGeneralService _generalService;
    private readonly ICustomerRepository _customerRepository;

    public LoanRequestService(
        ILoanRequestRepository loanRequestRepository,
        IApprovedLoanRepository approvedLoanRepository,
        IInstallmentRepository installmentRepository,
        IGeneralService generalService,
        ICustomerRepository customerRepository)
    {
        _loanRequestRepository = loanRequestRepository;
        _approvedLoanRepository = approvedLoanRepository;
        _installmentRepository = installmentRepository;
        _generalService = generalService;
        _customerRepository = customerRepository;

    }
    public async Task<string> CreateLoanRequest(CreateLoanRequest createLoanRequest)
    {
        var existingTerm = await _loanRequestRepository.GetByMonths(createLoanRequest.Months);
        var existingCustomer = await _customerRepository.GetCustomerById(createLoanRequest.CustomerId);

        var entity = createLoanRequest.Adapt<LoanRequest>();
        entity.Term = existingTerm!;
        entity.Customer = existingCustomer!;

        await _loanRequestRepository.AddLoanRequest(entity);
        return $"La solicitud de préstamo está siendo procesada. El Id de la solicitud es {entity.LoanId}";

    }

    public async Task<string> RejectedLoan(int loanId, string reason)
    {
        var loan = await _loanRequestRepository.GetLoanRequestById(loanId);
        loan.RequestStatus = "Rechazada";
        loan.RejectionReason = reason;
        await _loanRequestRepository.UpdateLoanRequestById(loan);

        return $"La solicitud fue rechazada por el siguiente motivo {loan.RejectionReason}";
    }



    public async Task<string> AproveLoan(int loanId, float interestRate)
    {
        var loanRequest = await _loanRequestRepository.GetLoanRequestById(loanId);

        loanRequest.RequestStatus = "Aprobado";

        var approvedLoan = loanRequest.Adapt<ApprovedLoan>();
        approvedLoan.InterestRate = interestRate;
        approvedLoan.PendingAmount = Math.Round(_generalService.CalculateTotalAmount(interestRate, approvedLoan.Amount, approvedLoan.Months));

        var installments = _generalService.GenerateInstallments(approvedLoan.ApprovalDate, approvedLoan.Amount, approvedLoan.InterestRate, approvedLoan.Months);
        await _approvedLoanRepository.AddApprovedLoan(approvedLoan);

        foreach (var installment in installments)
        {
            installment.ApprovedLoanId = approvedLoan.ApprovedLoanId;
            await _installmentRepository.AddInstallment(installment);
        }
        return $"La solicitud ha sido apropbada correctamente, el Id del prestamo es: {approvedLoan.ApprovedLoanId}";
    }
}


