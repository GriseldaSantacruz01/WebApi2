using Core.Entities;

namespace Core.DTOs.ApprovedLoan
{
    public class LoanDetailsResponse
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Profit { get; set; }
        public int Months { get; set; }
        public string Type { get; set; } = string.Empty;
        public float InterestRate { get; set; }
        public int PaidInstallments { get; set; }
        public int PendingInstallments { get; set; }
        public string NextDueDate { get; set; } = string.Empty;




    }
}
