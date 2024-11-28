using Core.DTOs.PaymentInstallment;
using FluentValidation;

namespace Infrastructure.Validations.Payment
{
    public class PaymentRequestValidation : AbstractValidator<PaymentRequestDto>
    {
        public PaymentRequestValidation()
        {
            RuleFor(x => x.LoanApprovedId)
                .NotEmpty();
            RuleFor(x => x.NumberOfInstallmentsToPay)
                .NotEmpty()
                .WithMessage("Debe ingresa uno o mas Id's de las cuotas")
                .WithErrorCode("INVALID_NUMBER");
        }
    }
}
