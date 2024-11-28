using Core.Entities;
using Mapster;

namespace Infrastructure.Mapping
{
    public class PaymentMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Installment, PaymentInstallment>();
        }
    }
}
