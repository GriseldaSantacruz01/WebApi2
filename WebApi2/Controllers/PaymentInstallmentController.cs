using Core.Interfaces.Repositories;
using WebApi.Controllers;

namespace WebApi2.Controllers
{
    public class PaymentInstallmentController : BaseApiController
    {
        private readonly IPaymentInstallmentRepository _paymentInstallamentRepository;

        public PaymentInstallmentController (IPaymentInstallmentRepository paymentInstallamentRepository)
        {
            _paymentInstallamentRepository = paymentInstallamentRepository;
        }
    }
}
