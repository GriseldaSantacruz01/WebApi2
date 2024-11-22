using Core.DTOs.Installment;
using Core.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebApi2.Controllers
{
    public class InstallmentController : BaseApiController
    {
        private readonly IInstallmentRepository _installamentRepository;

        public InstallmentController(IInstallmentRepository installamentRepository)
        {
            _installamentRepository = installamentRepository;

        }
        [HttpPost]
        public async Task<IActionResult> CreateInstallment([FromBody]SimulateInstallment simulateInstallment)
        {
            return Ok(await _installamentRepository.CreateInstallment(simulateInstallment));
        }

    }
}
