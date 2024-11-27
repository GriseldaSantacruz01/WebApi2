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

        public int InstallmentId { get; set; }
        public decimal InstallmentAmount { get; set; }
        public Installment Installment { get; set; } = null!;
    }
}
