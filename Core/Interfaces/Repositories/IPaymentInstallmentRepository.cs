using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IPaymentInstallmentRepository
{
    Task<PaymentInstallment> AddPaymentInstallment(PaymentInstallment payment);
    Task UpdatePaymentInstallment(PaymentInstallment payment);
}
