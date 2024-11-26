using Core.DTOs.LoanRequest;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using WebApi.Controllers;

namespace WebApi2.Controllers
{
    public class LoanRequestController : BaseApiController
    {
        private readonly IResponseService _responseService;
        private readonly ILoanRequestService _loanRequestService;
        public LoanRequestController(ILoanRequestService loanRequestService, IResponseService responseService)
        {
            _responseService = responseService;
            _loanRequestService = loanRequestService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateLoanRequest([FromBody]CreateLoanRequest createLoanRequest)
        {
           var customer = await _responseService.VerifyCustomer(createLoanRequest.CustomerId);
           var months = await _responseService.VerifyMonths(createLoanRequest.Months);
           if (months.Code == -1 || customer.Code == -1)
            {
                return NotFound(months.Message + " y " + customer.Message);
            }

            return Ok( await _loanRequestService.CreateLoanRequest(createLoanRequest));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/Approval/{loanId}")]

        public async Task<IActionResult> AproveLoan(int loanId, float interestRate)
        {
            var loan = await _responseService.VerifyLoanId(loanId);
            if (loan.Code == -1) return NotFound(loan.Message);


            return Ok(await _loanRequestService.AproveLoan(loanId, interestRate));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/Rejection/{loanId}")]

        public async Task<IActionResult> RejectedLoan(int loanId, string reason)
        {
            var loan = await _responseService.VerifyLoanId(loanId);
            if (loan.Code == -1) return NotFound(loan.Message);


            return Ok(await _loanRequestService.RejectedLoan(loanId, reason));
        }

    }
}
