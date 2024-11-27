
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using Mapster;

namespace Infrastructure.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IInstallmentRepository _installmentRepository;
        private readonly IPaymentInstallmentRepository _paymentInstallmentRepository;
        private readonly IGeneralService _generalService;
        private readonly IApprovedLoanRepository _approvedLoanRepository;

        public PaymentService 
            (IInstallmentRepository installmentRepository,
            IPaymentInstallmentRepository paymentInstallmentRepository,
            IGeneralService generalService,
            IApprovedLoanRepository approvedLoanRepository)
        {
            _installmentRepository = installmentRepository;
            _paymentInstallmentRepository = paymentInstallmentRepository;
            _generalService = generalService;
            _approvedLoanRepository = approvedLoanRepository;
        }
        public async Task<string> PayInstallmentsAsync(int loanApprovedId, int[] installmentIds)
        {
            var installments = await _installmentRepository.GetInstallments(loanApprovedId);
            var unpaidInstallments = installments
                .Where(i => i.PaymentDate == null).ToList();
            var installmentsToPay = unpaidInstallments
                .Where(i => installmentIds.Contains(i.InstallmentId)).ToList();
            decimal amount = 0;

            if (installmentsToPay.Count != installmentIds.Length)
            {
                return "Error: Algunas cuotas seleccionadas no existen o ya están pagadas.";
            }

            foreach (var installment in installmentsToPay)
            {
                installment.PaymentDate = DateTime.UtcNow;
                installment.InstallmentStatus = "Pagada";
                var payment = installment.Adapt<PaymentInstallment>();
                payment.NextDueDate = _generalService.CalculateNextDueDate(installment.DueDate, 1);
                amount += installment.InstallmentTotal ;
                await _paymentInstallmentRepository.AddAsync(payment);

            }
            var approvedLoan = await _approvedLoanRepository.GetLoanById(loanApprovedId);
            approvedLoan.PendingAmount -= amount;


            await _approvedLoanRepository.UpdateAsync(loanApprovedId);
            await _installmentRepository.UpdateAsync(installmentsToPay);

            return $"{installmentsToPay.Count} cuota(s) pagada(s) exitosamente.";
        }
    }
}
