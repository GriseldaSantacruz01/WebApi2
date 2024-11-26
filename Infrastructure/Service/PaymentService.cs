
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;

namespace Infrastructure.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IInstallmentRepository _installmentRepository;

        public PaymentService (IInstallmentRepository installmentRepository)
        {
            _installmentRepository = installmentRepository;
        }
        public async Task<string> PayInstallmentsAsync(int loanApprovedId, int[] installmentIds)
        {
            
            var installments = await _installmentRepository.GetInstallments(loanApprovedId);

            
            var unpaidInstallments = installments.Where(i => i.PaymentDate == null).ToList();

            var installmentsToPay = unpaidInstallments.Where(i => installmentIds.Contains(i.InstallmentId)).ToList();

            if (installmentsToPay.Count != installmentIds.Length)
            {
                return "Error: Algunas cuotas seleccionadas no existen o ya están pagadas.";
            }

            foreach (var installment in installmentsToPay)
            {
                installment.PaymentDate = DateTime.UtcNow; 
            }

            await _installmentRepository.UpdateAsync(installmentsToPay);

            return $"{installmentsToPay.Count} cuota(s) pagada(s) exitosamente.";
        }
    }
}
