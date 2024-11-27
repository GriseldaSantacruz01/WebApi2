

namespace Core.Entities;


public class Installment
{
    public int InstallmentId { get; set; }
    public decimal CapitalAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal InstallmentTotal { get; set; }
    public decimal InterestAmount {  get; set; }
    public string InstallmentStatus { get; set;  } = string.Empty;
    public DateTime DueDate { get; set; }
    public DateTime? PaymentDate { get; set; }

    public int ApprovedLoanId { get; set; }
    public ApprovedLoan ApprovedLoan { get; set; } = null!;
    public PaymentInstallment PaymentInstallment { get; set; } = null!;
}
