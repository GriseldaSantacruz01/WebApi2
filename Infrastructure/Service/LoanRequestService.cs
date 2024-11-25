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
    private readonly IGeneralService _termService;

    public LoanRequestService(
        ILoanRequestRepository loanRequestRepository,
        IApprovedLoanRepository approvedLoanRepository,
        IInstallmentRepository installmentRepository,
        IGeneralService termService)
    {
        _loanRequestRepository = loanRequestRepository;
        _approvedLoanRepository = approvedLoanRepository;
        _installmentRepository = installmentRepository;
        _termService = termService;
    }







    public async Task<string> CreateLoanRequest(CreateLoanRequest createLoanRequest, int customerId)
    {
        return await _loanRequestRepository.CreateLoanRequest(createLoanRequest, customerId);
    }

    public async Task<string> RejectedLoan(int loanId, string reason)
    {
        var loan = await _loanRequestRepository.GetByIdAsync(loanId);
        loan.RequestStatus = "Rechazada";
        loan.RejectionReason = reason;
        await _loanRequestRepository.UpdateAsync(loan);

        return $"La solicitud fue rechazada por el siguiente motivo {loan.RejectionReason}";
    }



    public async Task<string> AproveLoan(int loanId, float interestRate)
    {
        var loanRequest = await _loanRequestRepository.VerifyId(loanId);

        loanRequest.RequestStatus = "Aprobado";

        var approvedLoan = loanRequest.Adapt<ApprovedLoan>();
        approvedLoan.InterestRate = interestRate;

        var installments = _termService.GenerateInstallments(approvedLoan.ApprovalDate, approvedLoan.Amount, approvedLoan.InterestRate, approvedLoan.Months);
        await _approvedLoanRepository.AddAsync(approvedLoan);

        foreach (var installment in installments)
        {
            installment.ApprovedLoanId = approvedLoan.ApprovedLoanId;
            await _installmentRepository.AddAsync(installment);
        }
        return $"La solicitud ha sido apropbada correctamente, el Id del prestamo es: {approvedLoan.ApprovedLoanId}";
    }
}


