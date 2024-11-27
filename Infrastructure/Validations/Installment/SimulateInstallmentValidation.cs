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
            RuleFor(x => x.Months)
                .NotEmpty()
                .Must(month => month % 6 == 0)
                .WithMessage("El plazo debe ser en meses, seis meses (medio año) es el valor minimo")
                .WithErrorCode("INVALID_MONTHS");

        }
    }
}
