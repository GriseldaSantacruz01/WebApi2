using Core.DTOs.PaymentInstallment;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebApi2.Controllers
{
    public class PaymentInstallmentController : BaseApiController
    {
        private readonly IPaymentService _paymentInstallamenService;

        public PaymentInstallmentController (IPaymentService paymentInstallamentService)
        {
            _paymentInstallamenService = paymentInstallamentService;
        }

        [HttpPost("pay-installments")]
        public async Task<IActionResult> PayInstallments([FromBody] PaymentRequest request)
        {
            var result = await _paymentInstallamenService.PayInstallmentsAsync(request.ApprovedLoanId, request.InstallmentIds);

            if (result.StartsWith("Error"))
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
