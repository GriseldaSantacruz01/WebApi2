using Core.Interfaces.Repositories;
using WebApi.Controllers;

namespace WebApi2.Controllers
{
    public class ApprovedLoanController : BaseApiController
    {
        private readonly IApprovedLoan _approvedLoan;
        public ApprovedLoanController(IApprovedLoan approvedLoan)
        {
            _approvedLoan = approvedLoan;
        }
    }
}
