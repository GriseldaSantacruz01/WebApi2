using Core.Entities;

namespace Core.Interfaces.Service
{
    public interface ITermService
    {
        double CalculateInstallmentAmount(float interestRate, decimal amount, int months);
        DateTime CalculateNextDueDate(DateTime approvalDate, int monthsToAdd);
    }
}
