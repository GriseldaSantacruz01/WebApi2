using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebApi2.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GenerateToken ([FromQuery]string name, [FromQuery]string rol)
        {
            return Ok(_tokenService.GenerateToken(name, rol));

        }

    }
}
