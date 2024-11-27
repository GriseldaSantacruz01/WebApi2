using Core.DTOs.Installments;
using Core.DTOs.LoanRequest;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validations.LoanRequest
{
    public class RejectedRequestValidation : AbstractValidator<RejectedRequest>
    {
        public RejectedRequestValidation()
        {
            RuleFor(x => x.LoanId)
                .NotEmpty();
            RuleFor(x => x.Reason)
                .NotEmpty()
                .WithMessage("Un motivo de rechazo debe presentarse de manera obligatoria")
                .WithErrorCode("EMPTY_REASON");

        }
    }
}

