using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IPaymentInstallmentRepository
{
    Task AddPaymentInstallment(PaymentInstallment payment);
}
