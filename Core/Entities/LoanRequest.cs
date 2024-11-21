

using System.Globalization;

namespace Core.Entities;

public class LoanRequest
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public int LoanId { get; set; }
    public string Type { get; set; } = string.Empty;
    public TermIR Term { get; set; } = null!;
    public int TermId { get; set; }
    public string RequestStatus {  get; set; } = string.Empty ;
    public int FeedId{ get; set; }
    public Fee Fee { get; set; } = null!;
    public int FeesPaid { get; set; }
    public int FeesDue { get; set; }
    public int ApprovedLoanId { get; set; }
    public ApprovedLoan ApprovedLoan { get; set; } = null!;

}
