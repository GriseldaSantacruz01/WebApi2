using Core.Interfaces.Repositories;
using Infrastructure.Repositories;
using WebApi.Controllers;

namespace WebApi2.Controllers
{
    public class FeeController : BaseApiController
    {
        private readonly IFeeRepository _feeRepository;

        public FeeController(IFeeRepository feeRepository)
        {
            _feeRepository = feeRepository;

        }
    }
}
