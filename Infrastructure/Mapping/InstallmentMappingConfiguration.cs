﻿using Core.DTOs;
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
                .Map(dest => dest.DueDate, src => DateTime.UtcNow);

            config.NewConfig<Installment, SimulateInstallmentResponse>()
                .Map(dest => dest.InstallmentAmount, src => 0)
                .Map(dest => dest.TotalAmount, src => 0);

            config.NewConfig<Installment, InstallmentResponse>();

            config.NewConfig<Installment, PastDueInstallmentResponse>()
            .Map(dest => dest.DaysDelayed, src => (DateTime.UtcNow - src.DueDate).Days)
            .Map(dest => dest.PendingAmount, src => src.ApprovedLoan.PendingAmount)
            .Map(dest => dest.CustomerId, src => src.ApprovedLoan.CustomerId)
            .Map(dest => dest.CustomerName, src => $"{src.ApprovedLoan.Customer.FirstName} {src.ApprovedLoan.Customer.LastName}");
        }
    }
}
