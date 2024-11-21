namespace Core.Entities
{
    public class ApprovedLoan
    { 
        public int ApprovedLoanId { get; set; }
        public Customer Customer { get; set; } = null!;
        public int CustomerId { get; set; }
        public DateTime ApprovalDate { get; set; } 
        public Fee Fee { get; set; } = null!;
        public int FeeId { get; set; }
        public LoanRequest Loan { get; set; } = null!;
        public int LoanId { get; set; }
        public DateTime ExpirationDate {  get; set; } 
        public DateTime NextDueDate { get; set; }

    }
}
