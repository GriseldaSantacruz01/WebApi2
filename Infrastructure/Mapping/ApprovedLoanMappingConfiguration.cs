using Core.DTOs.ApprovedLoan;
using Core.Entities;
using Mapster;

namespace Infrastructure.Mapping
{
    public class ApprovedLoanMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ApprovedLoan, LoanDetailsResponse>()
                .Map(dest => dest.CustomerName, src => src.Customer.FirstName)
                .Map(dest => dest.TotalAmount, src => 0)
                .Map(dest => dest.Profit, src => 0)
                .Map(dest => dest.PaidInstallments, src => 0)
                .Map(dest => dest.PendingInstallments, src => 0)
                .Map(dest => dest.NextDueDate, src => string.Empty);
        }
    }
}
