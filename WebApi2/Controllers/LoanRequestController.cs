using Core.DTOs.LoanRequest;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using WebApi2.Controllers;

namespace WebApi2.Controllers
{
    public class LoanRequestController : BaseApiController
    {
        private readonly IResponseService _responseService;
        private readonly ILoanRequestService _loanRequestService;
        private readonly IValidator<CreateLoanRequest> _loanValidator;
        public LoanRequestController
            (ILoanRequestService loanRequestService, 
            IResponseService responseService,
            IValidator<CreateLoanRequest> loanValidator)
        {
            _responseService = responseService;
            _loanRequestService = loanRequestService;
            _loanValidator = loanValidator;
        }


        [HttpPost("api/CreateLoanRequest")]
        public async Task<IActionResult> CreateLoanRequest([FromBody]CreateLoanRequest createLoanRequest)
        {
            var validation = await _loanValidator.ValidateAsync(createLoanRequest);
            if (!validation.IsValid) return BadRequest(validation.Errors);

           var customer = await _responseService.VerifyCustomer(createLoanRequest.CustomerId);

           var months = await _responseService.VerifyMonths(createLoanRequest.Months);

           if (months.Code == -1 || customer.Code == -1) return NotFound(months.Message + " y " + customer.Message);
            
           return Ok( await _loanRequestService.CreateLoanRequest(createLoanRequest));

        }

        [Authorize(Roles = "Admin")]
        [HttpPost("api/Approval/{loanId}")]

        public async Task<IActionResult> AproveLoan([FromRoute]int loanId)
        {
            var loan = await _responseService.VerifyLoanId(loanId);
            if (loan.Code == -1) return NotFound(loan.Message);


            return Ok(await _loanRequestService.AproveLoan(loanId));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("api/Rejection/{loanId}")]

        public async Task<IActionResult> RejectedLoan([FromRoute]int loanId, [FromQuery]string reason)
        {
            var loan = await _responseService.VerifyLoanId(loanId);
            if (loan.Code == -1) return NotFound(loan.Message);

            return Ok(await _loanRequestService.RejectedLoan(loanId, reason));
        }

    }
}
