

using Core.DTOs.LoanRequest;
using FluentValidation;

namespace Infrastructure.Validations.LoanRequest
{
    public class ApprovedRequestValidation : AbstractValidator<ApprovedRequest>
    {
        public ApprovedRequestValidation()
        {
            RuleFor(x => x.LoanId)
                .NotEmpty();
            RuleFor(x => x.InterestRate)
                .NotEmpty()
                .WithMessage("La tasa de interes debe ser ingresada")
                .WithErrorCode("EMPTY_INTERESTRATE");
                
                
        }
    }
}
