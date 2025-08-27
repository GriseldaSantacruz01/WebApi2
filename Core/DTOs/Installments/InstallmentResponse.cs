using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Installments
{
    public class InstallmentResponse
    {
        public int InstallmentId { get; set; }
        public decimal InstallmentTotal { get; set; }
        public string InstallmentStatus { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
