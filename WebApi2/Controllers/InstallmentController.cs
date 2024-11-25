using Core.DTOs.Installments;
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

        public InstallmentController(IInstallmentService installamentService, IGeneralService termService, IResponseService responseService)
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

    }
}
