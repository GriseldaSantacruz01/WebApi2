namespace Core.Entities
{
    public class ApprovedLoan
    { 
        public int ApprovedLoanId { get; set; }
        public float InterestRate {  get; set; }
        public int Months {  get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime ApprovalDate { get; set; } 
        public Customer Customer { get; set; } = null!;
        public int CustomerId { get; set; }
        public Installment Installament { get; set; } = null!;
        public LoanRequest Loan { get; set; } = null!;
        public int LoanId { get; set; }
    }
}
