﻿

namespace Core.Entities;
public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<LoanRequest> Loans { get; set; } = null!;

}
