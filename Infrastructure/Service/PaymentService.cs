
using Core.DTOs.PaymentInstallment;
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
        private readonly IApprovedLoanRepository _approvedLoanRepository;

        public PaymentService 
            (IInstallmentRepository installmentRepository,
            IPaymentInstallmentRepository paymentInstallmentRepository,
            IApprovedLoanRepository approvedLoanRepository)
        {
            _installmentRepository = installmentRepository;
            _paymentInstallmentRepository = paymentInstallmentRepository;
            _approvedLoanRepository = approvedLoanRepository;
        }
        public async Task<string> PayInstallmentsAsync(PaymentDTO paymentDTO)
        {
            if (paymentDTO.NumberOfInstallmentsToPay <= 0)
                throw new ArgumentException("El número de cuotas a pagar debe ser mayor a 0.");

            var installments = await _installmentRepository.GetInstallmentsByApprovedLoanId(paymentDTO.ApprovedLoanId);

            if (!installments.Any())
                throw new InvalidOperationException("No hay cuotas pendientes para este préstamo aprobado.");

            if (installments.Count < paymentDTO.NumberOfInstallmentsToPay)
                throw new InvalidOperationException($"Solo hay {installments.Count} cuotas pendientes para pagar.");

            var installmentsToPay = installments
            .OrderBy(i => i.DueDate)
                .Take(paymentDTO.NumberOfInstallmentsToPay)
                .ToList();

            var totalAmount = installmentsToPay.Sum(i => i.InstallmentTotal);
            var nextInstallment = installments
                .OrderBy(i => i.DueDate)
                .FirstOrDefault(i => i.InstallmentStatus != "Pagada");

            var payment = paymentDTO.Adapt<PaymentInstallment>();
            payment.PaymentDate = DateTime.UtcNow;
            payment.InstallmentTotal = totalAmount;
            payment.NextDueDate = nextInstallment!.DueDate;
            await _paymentInstallmentRepository.AddPaymentInstallment(payment);

            foreach (var installment in installmentsToPay)
            {
                installment.InstallmentStatus = "Pagada";
                installment.PaymentInstallmentId = payment.PaymentInstallmentId;
                installment.PaymentDate = DateTime.UtcNow;
            }
            await _installmentRepository.UpdateInstallments(installments);
            var approvedLoan = await _approvedLoanRepository.GetLoanById(paymentDTO.ApprovedLoanId);
            approvedLoan.PendingAmount -= totalAmount;

            return $"{paymentDTO.NumberOfInstallmentsToPay} cuota(s han sido pagada(s) correctamente";
        }
    }
}
