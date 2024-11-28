using Core.DTOs.PaymentInstallment;
using FluentValidation;

namespace Infrastructure.Validations.Payment
{
    public class PaymentRequestValidation : AbstractValidator<PaymentRequest>
    {
        public PaymentRequestValidation()
        {
            RuleFor(x => x.ApprovedLoanId)
                .NotEmpty();
            RuleFor(x => x.InstallmentIds)
                .NotEmpty()
                .WithMessage("Debe ingresa uno o mas Id's de las cuotas")
                .WithErrorCode("INVALID_INSTALLMENTIDS");
        }
    }
}
