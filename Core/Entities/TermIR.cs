namespace Core.Entities;

public class TermIR
{
    public int TermId { get; set; }
    public int Months { get; set; }
    public float InterestRate { get; set; }
    public ApprovedLoan ApprovedLoan { get; set; } = null!;
    public LoanRequest LoanRequest { get; set; } = null!;
}
