using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Installments
{
    public class PastDueInstallmentResponse
    {
        public int InstallmentId { get; set; }
        public int ApprovedLoanId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public int DaysDelayed {  get; set; }
        public decimal PendingAmount { get; set; }
    }
}
