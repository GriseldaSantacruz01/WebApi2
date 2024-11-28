using Core.DTOs.PaymentInstallment;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi2.Controllers;

namespace WebApi2.Controllers
{
    public class PaymentInstallmentController : BaseApiController
    {
        private readonly IPaymentService _paymentInstallamenService;
        private readonly IValidator<PaymentDTO> _paymentRequestValidator;

        public PaymentInstallmentController (IPaymentService paymentInstallamentService, IValidator<PaymentDTO> paymentValidator)
        {
            _paymentInstallamenService = paymentInstallamentService;
            _paymentRequestValidator = paymentValidator;
        }

        [HttpPost("api/Pay")]
        public async Task<IActionResult> PayInstallments(PaymentDTO paymentDTO)
        {
            var paymentInstallment = await _paymentInstallamenService.PayInstallmentsAsync(paymentDTO);
            var validation = await _paymentRequestValidator.ValidateAsync(paymentDTO);
            if (!validation.IsValid) return BadRequest(validation.Errors);

            if (paymentInstallment.StartsWith("Error")) return NotFound(paymentInstallment);

            return Ok(paymentInstallment);
        }
    }
}
