using Core.Entities;

namespace Core.DTOs.Installment
{
    public class SimulateInstallmentDTO
    {
        public decimal Amount { get; set; }
        public int Months { get; set; }
        public float InterestRate { get; set; }
        

        public TermIR TermIR { get; set; } = null!;
    }
}
