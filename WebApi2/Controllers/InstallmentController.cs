using Core.DTOs.Installments;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using FluentValidation;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi2.Controllers
{
    public class InstallmentController : BaseApiController
    {
        private readonly IInstallmentService _installamentService;
        private readonly IResponseService _responseService;
        private readonly IValidator<SimulateInstallment> _simulateInstallmentValidator;

        public InstallmentController
            (IInstallmentService installamentService,
            IResponseService responseService,
            IValidator<SimulateInstallment>  simulateInstallmentValidator)
        {
            _installamentService = installamentService;
            _responseService = responseService;
            _simulateInstallmentValidator = simulateInstallmentValidator;
        }
        [HttpPost("api/Simulator")]
        public async Task<IActionResult> SimulateInstallment([FromBody] SimulateInstallment simulateInstallment)
        {
            var validation = await _simulateInstallmentValidator.ValidateAsync(simulateInstallment);
            if (!validation.IsValid) return BadRequest(validation.Errors);

            var verify = await _responseService.VerifyMonths(simulateInstallment.Months);
            if (verify.Code == -1 ) return NotFound(verify.Message);

            return Ok(await _installamentService.SimulateInstallment(simulateInstallment));
        }

        [HttpGet("api/GetInstallmentsByStatus/{approvedLoanId}")]
        public async Task<IActionResult> FilterByStatus([FromRoute]int approvedLoanId, [FromQuery]string filter = "all")
        {
            var approvedLoan = await _responseService.VerifyLoanApprovedId(approvedLoanId);
            if (approvedLoan.Code == -1 ) return NotFound(approvedLoan.Message);
            var installments = await _installamentService.FilterByStatus(approvedLoanId, filter);
            return Ok(installments);
        }

        [HttpGet("api/GetDelayedInstallments")]
        public async Task<IActionResult> DelayInstallmentList()
        {
            
            return Ok(await _installamentService.DelayInstallmentList());
        }
    }
}
