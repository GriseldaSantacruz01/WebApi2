using Core.DTOs.Installments;
using Core.Entities;

namespace Core.Interfaces.Service;

public interface IInstallmentService
{
    Task<SimulateInstallmentResponse> SimulateInstallment(SimulateInstallment simulateInstallment);
    Task<List<Installment>> GetInstallmentsByApprovedLoanId(int id);
    Task<List<InstallmentResponse>> FilterByStatus(int approvedLoanId, string filter);
    Task<List<PastDueInstallmentResponse>> DelayInstallmentList();


}
