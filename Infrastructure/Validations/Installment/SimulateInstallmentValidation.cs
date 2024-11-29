using Core.DTOs.Installments;
using FluentValidation;

namespace Infrastructure.Validations.Installment
{
    public class SimulateInstallmentValidation : AbstractValidator<SimulateInstallment>
    {
        public SimulateInstallmentValidation()
        {
            RuleFor(x => x.Amount)
                .NotEmpty()
                .GreaterThanOrEqualTo(1000000)
                .WithMessage("El monto no puede ser menos a un 1.000.000 y tampoco puede estar vacio")
                .WithErrorCode("INVALID_AMOUNT");
            

        }
    }
}
