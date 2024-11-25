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
                .Map(dest => dest.CustomerId, src => src.CustomerId)
                .Map(dest => dest.CustomerName, src => src.Customer.FirstName + " " + src.Customer.LastName)
                .Map(dest => dest.ApprovalDate, src => src.ApprovalDate)
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.TotalAmount, src => src.Installament.TotalAmount)
                .Map(dest => dest.Profit, src => src.Installament.InterestAmount)
                .Map(dest => dest.Months, src => src.Months)
                .Map(dest => dest.Type, src => src.Type)
                .Map(dest => dest.InterestRate, src => src.InterestRate)
                .Map(dest => dest.PaidInstallments, src => src.Months - src.Installament.RemainingInstallment)
                .Map(dest => dest.PendingInstallments, src => src.Installament.RemainingInstallment);
                //.Map(dest => dest.NextDueDate, src =>)
        }
    }
}
