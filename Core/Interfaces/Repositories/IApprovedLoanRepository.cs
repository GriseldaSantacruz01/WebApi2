using Core.DTOs;
using Core.DTOs.ApprovedLoan;
using Core.DTOs.LoanRequest;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IApprovedLoanRepository
    {
        Task AddAsync(ApprovedLoan approvedLoan);
        Task<ApprovedLoan> GetLoanById (int id);


    }
}
