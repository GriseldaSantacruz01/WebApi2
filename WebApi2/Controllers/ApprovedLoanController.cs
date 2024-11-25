using Core.DTOs.LoanRequest;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebApi2.Controllers
{
    public class ApprovedLoanController : BaseApiController
    {
        private readonly IApprovedLoanRepository _approvedLoan;
        public ApprovedLoanController(IApprovedLoanRepository approvedLoan)
        {
            _approvedLoan = approvedLoan;
        }

        
    }
}
