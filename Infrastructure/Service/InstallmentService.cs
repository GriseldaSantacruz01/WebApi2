using Core.DTOs.Installments;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;

namespace Infrastructure.Service;

public class InstallmentService : IInstallmentService
{
    private readonly IInstallmentRepository _installmentRepository;

    public InstallmentService(IInstallmentRepository installmentRepository)
    {
        _installmentRepository = installmentRepository;
    }

    public async Task<SimulateInstallmentResponse> CreateInstallment(SimulateInstallment simulateInstallment)
    {
        return await _installmentRepository.CreateInstallment(simulateInstallment);
    }

    public async Task<Response> VerifyMonths(int months)
    {
        var entity = await _installmentRepository.VerifyMonths(months);
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
            Message = "Se ha encontrado correctamente"
        };
    }

    public async Task<Installment> GetInstallment(int id)
    {
        return await _installmentRepository.GetInstallment(id);
    }

}
