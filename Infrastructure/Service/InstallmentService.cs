using Core.DTOs.Installments;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using Mapster;

namespace Infrastructure.Service;

public class InstallmentService : IInstallmentService
{
    private readonly IInstallmentRepository _installmentRepository;
    private readonly IResponseService _responseService;
    private readonly IGeneralService _generalService;

    public InstallmentService(IInstallmentRepository installmentRepository, IResponseService responseService, IGeneralService generalService)
    {
        _installmentRepository = installmentRepository;
        _responseService = responseService;
        _generalService = generalService;
    }

    public async Task<SimulateInstallmentResponse> CreateInstallment(SimulateInstallment simulateInstallment)
    {
        
        var response = simulateInstallment.Adapt<SimulateInstallmentResponse>();
        var term = await _installmentRepository.VerifyMonths(simulateInstallment.Months);
        response.InstallmentAmount = Math.Round(_generalService
            .CalculateInstallmentAmount(
            term.InterestRate, simulateInstallment.Amount, 
            simulateInstallment.Months));
        response.TotalAmount = Math.Round(response.InstallmentAmount * simulateInstallment.Months);
        return response;


    }

    public async Task<List<Installment>> GetInstallments(int id)
    {
        return await _installmentRepository.GetInstallments(id);
    }

    
}
