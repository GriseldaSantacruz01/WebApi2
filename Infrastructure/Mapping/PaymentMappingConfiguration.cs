using Core.Entities;
using Mapster;

namespace Infrastructure.Mapping
{
    public class PaymentMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Installment, PaymentInstallment>()
                .Map(dest => dest.InstallmentAmount, src => src.InstallmentTotal)
                .Map(dest => dest.NextDueDate, src => DateTime.UtcNow);
        }
    }
}
