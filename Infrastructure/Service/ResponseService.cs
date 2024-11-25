using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class ResponseService : IResponseService
    {
        private readonly ILoanRequestRepository _loanRequestRepository;
        private readonly IApprovedLoanRepository _approvedLoanRepository;
        private readonly IInstallmentRepository _installmentRepository;

        public ResponseService(
            ILoanRequestRepository loanRequestRepository,
            IApprovedLoanRepository approvedLoanRepository,
            IInstallmentRepository installmentRepository)
        {
            _loanRequestRepository = loanRequestRepository;
            _approvedLoanRepository = approvedLoanRepository;
            _installmentRepository = installmentRepository;

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
            var entity = await _loanRequestRepository.VerifyMonths(months);
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
            var entity = await _loanRequestRepository.VerifyCustomer(customer);
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
            var entity = await _loanRequestRepository.VerifyId(loanId);
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
