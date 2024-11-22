

namespace Core.Entities;


public class Installment
{
    public int InstallmentId { get; set; }
    public int ApprovedLoanId { get; set; }
    public ApprovedLoan ApprovedLoan { get; set; } = null!;
    public decimal Amount { get; set; }
    public int TermId { get; set; }
    public TermIR Term { get; set; } = null!;
    public decimal TotalAmount { get; set; }
    public decimal InstallmentAmount { get; set; }
    public string InstallmentStatus { get; set;  } = string.Empty;
    public int InstallmentDue { get; set; }
    public PaymentInstallment PaymentInstallment { get; set; } = null!;
}
