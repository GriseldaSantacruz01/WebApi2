using Core.DTOs.Installments;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service;

public class InstallmentService : IInstallmentService
{
    private readonly IInstallmentRepository _installmentRepository;
    private readonly ILoanRequestRepository _loanRequestRepository;
    private readonly IGeneralService _generalService;

    public InstallmentService
        (IInstallmentRepository installmentRepository,
        ILoanRequestRepository loanRequestRepository,
        IGeneralService generalService)
    {
        _installmentRepository = installmentRepository;
        _loanRequestRepository = loanRequestRepository;
        _generalService = generalService;
    }

    public async Task<SimulateInstallmentResponse> CreateInstallment(SimulateInstallment simulateInstallment)
    {
        
        var response = simulateInstallment.Adapt<SimulateInstallmentResponse>();
        var term = await _loanRequestRepository.VerifyMonths(simulateInstallment.Months);
        response.InstallmentAmount = Math.Round(_generalService
            .CalculateInstallmentAmount(
            term.InterestRate, simulateInstallment.Amount, 
            simulateInstallment.Months));
        response.TotalAmount = Math.Round(response.InstallmentAmount * simulateInstallment.Months);
        return response;


    }

    public async Task<List<InstallmentResponse>> FilterByStatus(int approvedLoanId, string filter)
    {
        var installments = await _installmentRepository.GetInstallments(approvedLoanId);
        switch (filter.ToLower())
        {
            case "all":
                break; 
            case "paid":
                installments = installments.Where(i => i.PaymentDate.HasValue).ToList();
                break;
            case "unpaid":
                installments = installments.Where(i => !i.PaymentDate.HasValue).ToList();
                break;
            default:
                throw new ArgumentException("Filtro invalido, filtros validos: all, paid, unpaid.");
        }

        return installments.Adapt<List<InstallmentResponse>>();
    }

    public async Task<List<Installment>> GetInstallments(int id)
    {
        return await _installmentRepository.GetInstallments(id);
    }

    public async Task<List<PastDueInstallmentResponse>> DelayInstallmentList(int approvedLoanId)
    {
        var installments = await _installmentRepository.GetDelayedInstallmentsWithLoanAndCustomer(approvedLoanId);

        var result = installments.Adapt<List<PastDueInstallmentResponse>>();

        return result;

    }

}
