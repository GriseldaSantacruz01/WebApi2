using Core.DTOs.Installments;
using Core.Entities;

namespace Core.Interfaces.Service;

public interface IInstallmentService
{
    Task<SimulateInstallmentResponse> CreateInstallment(SimulateInstallment simulateInstallment);

    Task<List<Installment>> GetInstallments(int id);

}
