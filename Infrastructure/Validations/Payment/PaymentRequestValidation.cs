using Core.DTOs.PaymentInstallment;
using FluentValidation;

namespace Infrastructure.Validations.Payment
{
    public class PaymentRequestValidation : AbstractValidator<PaymentDTO>
    {
        public PaymentRequestValidation()
        {
            RuleFor(x => x.ApprovedLoanId)
                .NotEmpty();
            RuleFor(x => x.NumberOfInstallmentsToPay)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Debe ingresar una cantidad valida")
                .WithErrorCode("INVALID_NUMBER");
        }
    }
}
