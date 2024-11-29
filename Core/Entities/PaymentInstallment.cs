using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PaymentInstallment
    {
        public int PaymentInstallmentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime NextDueDate { get; set; }

        public int NumberOfInstallmentsToPay { get; set; }
        public decimal InstallmentTotal { get; set; }
        public int ApprovedLoanId { get; set; }
        public ApprovedLoan ApprovedLoan { get; set; } = null!;
        public List<Installment> Installments { get; set; } = null!;
    }
}
