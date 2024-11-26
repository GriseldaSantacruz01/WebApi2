using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Service
{
    public interface IPaymentService
    {
        Task<string> PayInstallmentsAsync(int loanApprovedId, int[] installmentIds);
    }
}
