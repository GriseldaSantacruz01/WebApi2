namespace Core.DTOs.PaymentInstallment
{
    public class PaymentResponse
    {
        public int ApprovedLoanId { get; set; }
        public int PaidInstallments { get; set; } 
        public int RemainingInstallments {  get; set; }
        public string StatusMessage {  get; set; } = string.Empty;

    }
}
