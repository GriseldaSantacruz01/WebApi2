using Core.DTOs.Installments;
using Core.Entities;

namespace Core.Interfaces.Service;

public interface IInstallmentService
{
    Task<SimulateInstallmentResponse> CreateInstallment(SimulateInstallment simulateInstallment);
    Task<Response> VerifyMonths(int months);
    Task<Installment> GetInstallment(int id);

}
