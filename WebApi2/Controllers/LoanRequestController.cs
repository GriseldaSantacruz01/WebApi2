using Core.DTOs.LoanRequest;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using FluentValidation;
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
        private readonly IValidator<CreateLoanRequest> _loanValidator;
        private readonly IValidator<ApprovedRequest> _approveValidator;
        private readonly IValidator<RejectedRequest> _rejectedValidator;
        public LoanRequestController
            (ILoanRequestService loanRequestService, 
            IResponseService responseService,
            IValidator<CreateLoanRequest> loanValidator,
            IValidator<ApprovedRequest> approveValidator,
            IValidator<RejectedRequest> rejectedValidator)
        {
            _responseService = responseService;
            _loanRequestService = loanRequestService;
            _loanValidator = loanValidator;
            _approveValidator = approveValidator;
            _rejectedValidator = rejectedValidator;
        }


        [HttpPost("/CreateLoanRequest")]
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
        [HttpPost("/Approval/{loanId}")]

        public async Task<IActionResult> AproveLoan(ApprovedRequest approvedRequest)
        {
            var validation = await _approveValidator.ValidateAsync(approvedRequest);
            if (!validation.IsValid) return BadRequest(validation.Errors);

            var loan = await _responseService.VerifyLoanId(approvedRequest.LoanId);
            if (loan.Code == -1) return NotFound(loan.Message);


            return Ok(await _loanRequestService.AproveLoan(approvedRequest.LoanId, approvedRequest.InterestRate));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/Rejection")]

        public async Task<IActionResult> RejectedLoan(RejectedRequest rejectedRequest)
        {
            var validation = await _rejectedValidator.ValidateAsync(rejectedRequest);
            if (!validation.IsValid) return BadRequest(validation.Errors);

            var loan = await _responseService.VerifyLoanId(rejectedRequest.LoanId);
            if (loan.Code == -1) return NotFound(loan.Message);


            return Ok(await _loanRequestService.RejectedLoan(rejectedRequest.LoanId, rejectedRequest.Reason));
        }

    }
}
