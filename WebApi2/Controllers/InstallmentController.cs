using Core.DTOs.Installments;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebApi2.Controllers
{
    public class InstallmentController : BaseApiController
    {
        private readonly IInstallmentService _installamentService;
        private readonly IResponseService _responseService;

        public InstallmentController(IInstallmentService installamentService,  IResponseService responseService)
        {
            _installamentService = installamentService;
            _responseService = responseService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateInstallment([FromBody] SimulateInstallment simulateInstallment)
        {
            var verify = await _responseService.VerifyMonths(simulateInstallment.Months);
            if (verify.Code == -1 ) return NotFound(verify.Message);

            return Ok(await _installamentService.CreateInstallment(simulateInstallment));
        }

        [HttpGet("/GetInstallments/{approvedLoanId}")]
        public async Task<IActionResult> FilterByStatus([FromRoute]int approvedLoanId, [FromQuery]string filter)
        {
            var approvedLoan = await _responseService.VerifyLoanApprovedId(approvedLoanId);
            if (approvedLoan.Code == -1 ) return NotFound(approvedLoan.Message);
            var installments = await _installamentService.FilterByStatus(approvedLoanId, filter);
            return Ok(installments);
        }
    }
}
