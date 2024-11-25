

using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service
{
    public class TermService : ITermService
    {
        private readonly AplicationDbContext _context;
        public TermService(AplicationDbContext context)
        {
            _context = context;
        }
        
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
            DateTimeOffset approvalDateOffset = new(approvalDate, TimeZoneInfo.Local.GetUtcOffset(approvalDate));

            DateTimeOffset nextDueDateOffset = approvalDateOffset.AddMonths(monthsToAdd);

            DateTimeOffset firstDayOfNextMonth = new(nextDueDateOffset.Year, nextDueDateOffset.Month, 1, 0, 0, 0, nextDueDateOffset.Offset);

            return firstDayOfNextMonth.DateTime;
        }

    }
}
