

using System.Globalization;

namespace Core.Entities;

public class LoanRequest
{
    public int LoanId { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public TermIR Term { get; set; } = null!;
    public int TermId { get; set; }
    public int Months { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = string.Empty;
    public string RequestStatus {  get; set; } = string.Empty ;
    public int ApprovedLoanId { get; set; }
    public ApprovedLoan ApprovedLoan { get; set; } = null!;

}
