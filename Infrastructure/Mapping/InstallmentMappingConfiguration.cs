using Core.DTOs;
using Core.DTOs.Installment;
using Core.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Mapping
{
    public class InstallmentMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<SimulateInstallment, SimulateInstallmentDTO>()
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.TermIR.Months, src => src.Months);
            config.NewConfig<SimulateInstallmentDTO, SimulateInstallmentResponse>()
                .Map(dest => dest.InstallmentAmount, src => CalculateInstallmentAmount(src.InterestRate, src.Months, src.Amount))
                .Map(dest => dest.TotalAmount, src => CalculateInstallmentAmount(src.InterestRate, src.Months, src.Amount) * src.Months);
            config.NewConfig<TermIR, TermDTO>();
            config.NewConfig<TermDTO, SimulateInstallmentDTO>();
            
            
        }



        public static double CalculateInstallmentAmount (float interestRate, int months, decimal amount)
        {
            var interest = (double)interestRate/ 12 / 100;
            var InstallmentAmount = ((double)amount * interest * Math.Pow(1 + interest, months)) / (Math.Pow(1 + interest, months) - 1);
            return InstallmentAmount;
        }
    }
}
