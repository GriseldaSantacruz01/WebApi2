namespace Core.Entities
{
    public class ApprovedLoan
    { 
        public int ApprovedLoanId { get; set; }
        public Customer Customer { get; set; } = null!;
        public int CustomerId { get; set; }
        public DateTime ApprovalDate { get; set; } 
        public Installment Installament { get; set; } = null!;
        public int InstallamentId { get; set; }
        public LoanRequest Loan { get; set; } = null!;
        public int LoanId { get; set; }
        public TermIR Term { get; set; } = null!;
        public int TermId { get; set; }
        public DateTime ExpirationDate {  get; set; } 
        public DateTime NextDueDate { get; set; }
        public decimal TotalFeeAmount { get; set; }
        public decimal CapitalAmount { get; set; }
        public float InterestAmount { get; set; }
        public float InterestRate {  get; set; }

    }
}
