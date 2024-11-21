using Core.Interfaces.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FeeRepository : IFeeRepository
    {
        private readonly ApplicationDbContext _context;

        public FeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
