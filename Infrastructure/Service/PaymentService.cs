
using Core.DTOs.PaymentInstallment;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using Mapster;
using System.Linq;

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
        public async Task<string> PayInstallmentsAsync(PaymentRequestDto paymentRequest)
        {
            if (paymentRequest.NumberOfInstallmentsToPay <= 0)
                throw new ArgumentException("El número de cuotas a pagar debe ser mayor a 0.");

            var installments = await _installmentRepository.GetInstallmentsByApprovedLoanId(paymentRequest.LoanApprovedId);

            if (!installments.Any())
                throw new InvalidOperationException("No hay cuotas pendientes para este préstamo aprobado.");

            if (installments.Count < paymentRequest.NumberOfInstallmentsToPay)
                throw new InvalidOperationException($"Solo hay {installments.Count} cuotas pendientes para pagar.");

            var installmentsToPay = installments
                .OrderBy(i => i.DueDate)
                .Take(paymentRequest.NumberOfInstallmentsToPay)
                .ToList();

            var totalAmount = installmentsToPay.Sum(i => i.TotalAmount);
            var nextInstallment = installments
                .OrderBy(i => i.DueDate)
                .FirstOrDefault(i => i.InstallmentStatus != "Pagada");

            var payment = paymentRequest.Adapt<PaymentInstallment>();
            payment.PaymentDate = DateTime.UtcNow;
            payment.InstallmentTotal = totalAmount;
            payment.NextDueDate = nextInstallment!.DueDate;

            foreach (var installment in installmentsToPay)
            {
                installment.InstallmentStatus = "Pagada";
                installment.PaymentInstallmentId = payment.PaymentInstallmentId;
                installment.PaymentDate = DateTime.UtcNow;
            }
            await _installmentRepository.UpdateInstallments(installments);


            await _paymentInstallmentRepository.AddPaymentInstallment(payment);

            return $"{paymentRequest.NumberOfInstallmentsToPay} cuota(s han sido pagada(s) correctamente";
        }
    
    }
}
