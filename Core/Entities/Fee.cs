

namespace Core.Entities;


public class Fee
{
    public int LoanId { get; set; }
    public LoanRequest Loan { get; set; } = null!;
    public int FeeId { get; set; }
    public decimal Amount { get; set; }
    public int TermId { get; set; }
    public TermIR Term { get; set; } = null!;
    public decimal TotalAmount { get; set; }
    public decimal FeeAmount { get; set; }
    public string FeeStatus { get; set;  } = string.Empty;
}
