namespace Core.DTOs.LoanRequest
{
    public class ApprovedRequest
    {
        public int LoanId { get; set; }
        public float InterestRate { get; set; }

    }
    public class RejectedRequest
    {
        public int LoanId { get; set; }
        public string Reason { get; set; } = string.Empty;

    }
}
