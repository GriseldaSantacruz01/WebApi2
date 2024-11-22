using Core.DTOs.Installment;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class InstallmentRepository : IInstallmentRepository
    {
        private readonly AplicationDbContext _context;

        public InstallmentRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SimulateInstallmentResponse> CreateInstallment(SimulateInstallment simulateInstallment)
        {
            var entity = simulateInstallment.Adapt<SimulateInstallmentDTO>();//revisado
            return entity.Adapt<SimulateInstallmentResponse>();
        }

        public async Task<TermIR> VerifyMonths(SimulateInstallment simulateInstallment)
        {
            var entity = await _context.TermIRs.FirstOrDefaultAsync(x => x.Months == simulateInstallment.Months);
            if (entity == null) throw new Exception("No existe el plazo que ha ingresado, recuerde ingresar en meses el plazo");
            return entity;
        }
       
    }
}
