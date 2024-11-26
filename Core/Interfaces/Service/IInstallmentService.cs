using Core.DTOs.Installments;
using Core.Entities;

namespace Core.Interfaces.Service;

public interface IInstallmentService
{
    Task<SimulateInstallmentResponse> CreateInstallment(SimulateInstallment simulateInstallment);

    Task<List<Installment>> GetInstallments(int id);
    Task<List<InstallmentResponse>> FilterByStatus(int approvedLoanId, string filter);
    Task<List<PastDueInstallmentResponse>> DelayInstallmentList(int approvedLoanId,);


}
