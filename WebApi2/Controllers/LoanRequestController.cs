using Core.Interfaces.Repositories;
using WebApi.Controllers;

namespace WebApi2.Controllers
{
    public class LoanRequestController : BaseApiController
    {
        private readonly ILoanRequestRepository _loanRequestRepository;

        public LoanRequestController(ILoanRequestRepository loanRequestRepository)
        {
            _loanRequestRepository = loanRequestRepository;
        }

    }
}
