using Core.DTOs.PaymentInstallment;

namespace Core.Interfaces.Service
{
    public interface IPaymentService
    {
        Task<string> PayInstallmentsAsync(PaymentRequestDto paymentRequestDto);
    }
}
