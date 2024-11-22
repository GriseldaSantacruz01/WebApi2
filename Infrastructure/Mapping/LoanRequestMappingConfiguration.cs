using Core.DTOs.LoanRequest;
using Core.Entities;
using Mapster;

namespace Infrastructure.Mapping;

public class LoanRequestMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateLoanRequest, LoanRequest>()
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Term.Months, src => src.Months)
            .Map(dest => dest.Type, src => "Pendiente de aprobacion");

    }
}
