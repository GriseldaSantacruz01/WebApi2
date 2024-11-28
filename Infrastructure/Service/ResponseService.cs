using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;

namespace Infrastructure.Service
{
    public class ResponseService : IResponseService
    {
        private readonly ILoanRequestRepository _loanRequestRepository;
        private readonly IApprovedLoanRepository _approvedLoanRepository;
        private readonly ICustomerRepository _customerRepository;

        public ResponseService(
            ILoanRequestRepository loanRequestRepository,
            IApprovedLoanRepository approvedLoanRepository,
            ICustomerRepository customerRepository)
        {
            _loanRequestRepository = loanRequestRepository;
            _approvedLoanRepository = approvedLoanRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Response> VerifyLoanApprovedId(int loanId)
        {
            var entity = await _approvedLoanRepository.GetLoanById(loanId);
            if (entity == null)
            {
                return new Response
                {
                    Code = -1,
                    Message = "No se ha encontrado el prestamo"
                };
            }
            return new Response
            {
                Code = 1,
                Message = "Se ha encontrado correctamente el prestamo"
            };
        }

        public async Task<Response> VerifyMonths(int months)
        {
            var entity = await _loanRequestRepository.GetByMonths(months);
            if (entity == null)
            {
                return new Response
                {
                    Code = -1,
                    Message = "No se ha ingresado un valor existente para el plazo"
                };
            }
            return new Response
            {
                Code = 1,
                Message = "Se ha encontrado correctamente el plazo"
            };
        }

        public async Task<Response> VerifyCustomer(int customer)
        {
            var entity = await _customerRepository.GetCustomerById(customer);
            if (entity == null)
            {
                return new Response
                {
                    Code = -1,
                    Message = "No se ha encontrado al cliente"
                };
            }
            return new Response
            {
                Code = 1,
                Message = "Se ha encontrado correctamente al cliente"
            };
        }

        public async Task<Response> VerifyLoanId(int loanId)
        {
            var entity = await _loanRequestRepository.GetLoanRequestById(loanId);
            if (entity == null)
            {
                return new Response
                {
                    Code = -1,
                    Message = "No se ha encontrado el prestamo"
                };
            }
            return new Response
            {
                Code = 1,
                Message = "Se ha encontrado correctamente el prestamo"
            };
        }


    }
}
