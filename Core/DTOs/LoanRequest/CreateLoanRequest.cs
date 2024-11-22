namespace Core.DTOs.LoanRequest
{
    public class CreateLoanRequest
    {
        public decimal Amount { get; set; }
        public int Months { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
