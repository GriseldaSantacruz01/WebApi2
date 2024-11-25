using Core.DTOs;
using Core.DTOs.Installments;
using Core.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Mapping
{
    public class InstallmentMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ApprovedLoan, Installment>()
                .Map(dest => dest.ApprovedLoanId, src => src.ApprovedLoanId)
                .Map(dest => dest.TotalAmount, src => 0)
                .Map(dest => dest.CapitalAmount, src => src.Amount)
                .Map(dest => dest.InterestAmount, src => 0)
                .Map(dest => dest.InstallmentTotal, src => 0)
                .Map(dest => dest.RemainingInstallment, src => src.Months)
                .Map(dest => dest.DueDate, src => DateTime.UtcNow)
                .Map(dest => dest.InstallmentStatus, src => $"Hay cuotas pendientes");
            



        }



    }
}
