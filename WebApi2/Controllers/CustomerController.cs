using Core.Interfaces.Repositories;
using WebApi.Controllers;

namespace WebApi2.Controllers;

public class CustomerController : BaseApiController
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
        
    }

}
