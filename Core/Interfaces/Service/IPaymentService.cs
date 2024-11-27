namespace Core.Interfaces.Service
{
    public interface IPaymentService
    {
        Task<string> PayInstallmentsAsync(int loanApprovedId, int[] installmentIds);
    }
}
