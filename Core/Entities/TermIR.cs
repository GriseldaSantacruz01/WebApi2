namespace Core.Entities;

public class TermIR
{
    public int TermId { get; set; }
    public int Months { get; set; }
    public decimal InterestRate { get; set; }
    public List<Fee> Fees { get; set; } = null!;

}
