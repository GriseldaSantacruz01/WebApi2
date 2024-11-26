using Core.Entities;

namespace Core.Interfaces.Service
{
    public interface IGeneralService
    {
        decimal CalculateInstallmentAmount(float interestRate, decimal amount, int months);
        decimal CalculateTotalAmount(float interestRate, decimal amount, int months);
        DateTime CalculateNextDueDate(DateTime approvalDate, int monthsToAdd);
        List<Installment> GenerateInstallments(DateTime approvalDate, decimal amount, float interestRate, int months);
        
    }
}
