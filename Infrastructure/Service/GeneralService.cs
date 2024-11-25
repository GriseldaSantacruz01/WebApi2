﻿

using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service
{
    public class GeneralService : IGeneralService
    {
        public double CalculateInstallmentAmount(float interestRate, decimal amount, int months)
        {
            if (interestRate == 0 || months == 0 || amount <= 0) throw new Exception("Los valores son nulos, no se pudo realizar el calculo");


            var interest = (double)interestRate / 12 / 100;
            if (interest <= 0) throw new Exception("El interes usado no es valido");

            var numerator = (double)amount * interest * Math.Pow(1 + interest, months);
            var denominator = Math.Pow(1 + interest, months) - 1;

            if (denominator == 0) throw new Exception("El calculo ha dado un resultado invalido");

            return numerator / denominator;
        }

        public DateTime CalculateNextDueDate(DateTime approvalDate, int monthsToAdd)
        {
            var nextDueDate = new DateTime(approvalDate.Year, approvalDate.Month, 1)
                         .AddMonths(monthsToAdd)
                         .ToUniversalTime(); 
            return nextDueDate; ;
        }

        public List<Installment> GenerateInstallments(DateTime approvalDate, decimal amount, float interestRate, int months)
        {
            var installments = new List<Installment>();
            var installmentAmount = (decimal)CalculateInstallmentAmount(interestRate, amount, months);
            var totalAmount = installmentAmount * months;
            for (int i = 1; i <= months; i++)
            {
                var dueDate = CalculateNextDueDate(approvalDate, i);
                installments.Add(new Installment
                {
                    InstallmentTotal = Math.Round(installmentAmount),
                    CapitalAmount = Math.Round(amount),
                    TotalAmount = Math.Round(totalAmount),
                    InterestAmount = Math.Round(totalAmount - amount),
                    DueDate = dueDate,
                    InstallmentStatus = "Pendiente"
                });

            }
            return installments;
        }

       
    }
}