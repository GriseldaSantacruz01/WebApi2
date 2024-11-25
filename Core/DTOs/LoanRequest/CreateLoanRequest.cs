namespace Core.DTOs.LoanRequest
{
    public class CreateLoanRequest
    {
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public int Months { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
