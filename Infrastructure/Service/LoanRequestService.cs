using Core.DTOs.LoanRequest;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using Infrastructure.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service;

public class LoanRequestService : ILoanRequestService
{
    private readonly ILoanRequestRepository _loanRequestRepository;
    private readonly IApprovedLoanRepository _approvedLoanRepository;
    private readonly IInstallmentRepository _installmentRepository;
    private readonly ITermService _termService;

    public LoanRequestService(
        ILoanRequestRepository loanRequestRepository,
        IApprovedLoanRepository approvedLoanRepository,
        IInstallmentRepository installmentRepository,
        ITermService termService)
    {
        _loanRequestRepository = loanRequestRepository;
        _approvedLoanRepository = approvedLoanRepository;
        _installmentRepository = installmentRepository;
        _termService = termService;
    }



    public async Task<Response> VerifyMonths(int months)
    {
        var entity = await _loanRequestRepository.VerifyMonths(months);
        if (entity == null)
        {
            return new Response
            {
                Code = -1,
                Message = "No se ha ingresado un valor existente para el plazo"
            };
        }
        return new Response
        {
            Code = 1,
            Message = "Se ha encontrado correctamente el plazo"
        };
    }

    public async Task<Response> VerifyCustomer(int customer)
    {
        var entity = await _loanRequestRepository.VerifyCustomer(customer);
        if (entity == null)
        {
            return new Response
            {
                Code = -1,
                Message = "No se ha encontrado al cliente"
            };
        }
        return new Response
        {
            Code = 1,
            Message = "Se ha encontrado correctamente al cliente"
        };
    }

    public async Task<Response> VerifyId(int loanId)
    {
        var entity = await _loanRequestRepository.VerifyId(loanId);
        if (entity == null)
        {
            return new Response
            {
                Code = -1,
                Message = "No se ha encontrado el prestamo"
            };
        }
        return new Response
        {
            Code = 1,
            Message = "Se ha encontrado correctamente el prestamo"
        };
    }



    public async Task<string> CreateLoanRequest(CreateLoanRequest createLoanRequest, int customerId)
    {
        return await _loanRequestRepository.CreateLoanRequest(createLoanRequest, customerId);
    }

    public async Task<string> HandleApprovalOrRejectionAsync(RequestResponse request)
    {
        var loanRequest = await _loanRequestRepository.VerifyId(request.LoanId);



        if (request.Approve)
        {
            loanRequest.RequestStatus = "Aprobado";

            var approvedLoan = loanRequest.Adapt<ApprovedLoan>();
            approvedLoan.InterestRate = request.InterestRate;

            await _approvedLoanRepository.AddAsync(approvedLoan);

            var installment = approvedLoan.Adapt<Installment>();

            installment.ApprovedLoanId = approvedLoan.ApprovedLoanId;
            installment.InstallmentTotal = (decimal)_termService.CalculateInstallmentAmount(approvedLoan.InterestRate, approvedLoan.Amount, approvedLoan.Months);
            installment.TotalAmount = installment.InstallmentTotal * approvedLoan.Months;
            installment.InterestAmount = installment.TotalAmount - installment.CapitalAmount;
            installment.DueDate =  _termService.CalculateNextDueDate(approvedLoan.ApprovalDate, approvedLoan.Months - approvedLoan.Installament.RemainingInstallment);

            await _installmentRepository.AddAsync(installment);

            return $"La solicitud ha sido apropbada correctamente, el Id del prestamo es: {approvedLoan.ApprovedLoanId}";
        }
        else
        {
           if (string.IsNullOrWhiteSpace(request.Reason))
            {
                throw new Exception("Es necesario un motivo de rechazo");
            }

            loanRequest.RequestStatus = "Rechazada";
            loanRequest.RejectionReason = request.Reason;
            await _loanRequestRepository.UpdateAsync(loanRequest);

            return $"La solicitud fue rechazada por el siguiente motivo {loanRequest.RejectionReason}";
        }

    }
}
