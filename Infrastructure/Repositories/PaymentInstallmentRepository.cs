using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories
{
    public class PaymentInstallmentRepository : IPaymentInstallmentRepository
    {
        private readonly AplicationDbContext _context;
        public PaymentInstallmentRepository(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddPaymentInstallment(PaymentInstallment payment)
        {
            _context.PaymentInstallments.Add(payment);
            await _context.SaveChangesAsync();
        }
    }
}
