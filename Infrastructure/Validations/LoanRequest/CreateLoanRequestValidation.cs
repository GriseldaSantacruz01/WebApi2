using Core.DTOs.LoanRequest;
using FluentValidation;

namespace Infrastructure.Validations.LoanRequest
{
    public class CreateLoanRequestValidation : AbstractValidator<CreateLoanRequest>
    {
        public CreateLoanRequestValidation() 
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
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("El CustomerId es un parametro requerido y tiene que ser mayor a cero")
                .WithErrorCode("INVALID_CUSTOMERID");
            RuleFor(x => x.Type)
                .NotEmpty()
                .Matches(@"^[a-zA-Z0-9\s]*$")
                .WithMessage("El nombre solo puede contener letras, números espacios.")
                .WithErrorCode("INVALID_TYPE");

        }
    }
}
