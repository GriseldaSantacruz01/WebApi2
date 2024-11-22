using Core.DTOs.Installment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IInstallmentRepository
    {
        Task<SimulateInstallmentResponse> CreateInstallment(SimulateInstallment simulateInstallment);
    }
}
