using Core.Entities;

namespace Core.DTOs.Installments
{
    public class SimulateInstallmentDTO
    {
        public decimal Amount { get; set; }
        public int Months { get; set; }
        public float InterestRate { get; set; }


    }
}
