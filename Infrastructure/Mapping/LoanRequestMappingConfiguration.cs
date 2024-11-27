using Core.DTOs.LoanRequest;
using Core.Entities;
using Mapster;

namespace Infrastructure.Mapping;

public class LoanRequestMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateLoanRequest, LoanRequest>()
            .Map(dest => dest.Customer.CustomerId, src => src.CustomerId)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Term.Months, src => src.Months)
            .Map(dest => dest.Type, src => src.Type)
            .Map(dest => dest.RequestStatus, src => "Pendiente");
        config.NewConfig<LoanRequest, ApprovedLoan>()
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.ApprovalDate, src => DateTime.UtcNow)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Months, src => src.Months)
            .Map(dest => dest.Type, src => src.Type)
            .Map(dest => dest.InterestRate, src => 0)
            .Map(dest => dest.PendingAmount, src => 0);

    }
}
