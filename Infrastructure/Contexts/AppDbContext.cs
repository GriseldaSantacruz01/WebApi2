

using Core.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Infrastructure.Contexts
{
    public partial class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<TermIR> TermIRs { get; set; }

        public DbSet<Fee> Fees { get; set; }

        public DbSet<LoanRequest> LoanRequests { get; set; }
        public DbSet<ApprovedLoan> ApprovedLoans { get; set; }
        


        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TermIRConfiguration());
            modelBuilder.ApplyConfiguration(new FeeConfiguration());
            modelBuilder.ApplyConfiguration(new LoanRequestConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new ApprovedLoanConfiguration());


        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
