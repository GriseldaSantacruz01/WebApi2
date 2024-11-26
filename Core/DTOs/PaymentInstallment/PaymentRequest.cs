namespace Core.DTOs.PaymentInstallment
{
    public class PaymentRequest
    {
        public int ApprovedLoanId { get; set; }
        public int[] InstallmentIds { get; set; } = []!; 
    }
}
