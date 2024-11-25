namespace Core.DTOs.LoanRequest
{
    public class RequestResponse
    {
        public int LoanId { get; set; }
        public bool Approve {  get; set; }
        public string? Reason { get; set; }
        public float InterestRate { get; set; }

    }
}
