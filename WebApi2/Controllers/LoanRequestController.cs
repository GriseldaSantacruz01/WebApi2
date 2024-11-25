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
        private readonly ILoanRequestRepository _loanRequestRepository;
        private readonly ILoanRequestService _loanRequestService;
        public LoanRequestController(ILoanRequestRepository loanRequestRepository, ILoanRequestService loanRequestService)
        {
            _loanRequestRepository = loanRequestRepository;
            _loanRequestService = loanRequestService;
        }


        [HttpPost("/{customerId}")]
        public async Task<IActionResult> CreateLoanRequest([FromBody]CreateLoanRequest createLoanRequest, int customerId)
        {
           var customer = await _loanRequestService.VerifyCustomer(customerId);
           var months = await _loanRequestService.VerifyMonths(createLoanRequest.Months);
           if (months.Code == -1 || customer.Code == -1)
            {
                return NotFound(months.Message + " y " + customer.Message);
            }

            return Ok( await _loanRequestService.CreateLoanRequest(createLoanRequest, customerId));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/ApprovalOrRejection/{loanId}")]

        public async Task<IActionResult> HandleApprovalOrRejectionAsync(RequestResponse loanRequest)
        {
            var loan = await _loanRequestService.VerifyId(loanRequest.LoanId);
            if (loan.Code == -1) return NotFound(loan.Message);
            if (!loanRequest.Approve && string.IsNullOrEmpty(loanRequest.Reason) && loanRequest.InterestRate != 0) 
            { 
                return BadRequest("Si un prestamo es rechazado se necesita un motivo pero no una tasa de interes"); 

            }
            else
            {
                if (loanRequest.Approve && loanRequest.InterestRate == 0) return BadRequest("Si es aprobado no necesita un motivo pero si una tasa de interes"); 
            }
            
            return Ok(await _loanRequestService.HandleApprovalOrRejectionAsync(loanRequest));
        }



    }
}
