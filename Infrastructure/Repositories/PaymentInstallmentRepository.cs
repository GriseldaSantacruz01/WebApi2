using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PaymentInstallmentRepository : IPaymentInstallmentRepository
    {
        private readonly AplicationDbContext _context;
        public PaymentInstallmentRepository(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync (PaymentInstallment payment)
        {
            await _context.AddAsync(payment);
        }
    }
}
