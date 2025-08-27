using Core.DTOs.LoanRequest;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi2.Controllers
{
    public class ApprovedLoanController : BaseApiController
    {
        private readonly IApprovedLoanService _approvedLoanService;
        private readonly IResponseService _responseService;
        public ApprovedLoanController(IApprovedLoanService approvedLoanService, IResponseService responseService)
        {
            _approvedLoanService = approvedLoanService;
            _responseService = responseService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("api/GetApprovedLoanDetails/{loanId}")]
        public async Task<IActionResult> GetLoanDetailed([FromRoute]int loanId)
        {
            var loanApproved = await _responseService.VerifyLoanApprovedId(loanId);
            if (loanApproved.Code == -1) return NotFound(loanApproved.Message);
            return Ok(await _approvedLoanService.GetApprovedLoanDetails(loanId));
        }
    }
}
