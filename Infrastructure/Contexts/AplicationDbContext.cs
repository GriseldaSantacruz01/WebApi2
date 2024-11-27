

using Core.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public partial class AplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<TermIR> TermIRs { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<LoanRequest> LoanRequests { get; set; }
        public DbSet<ApprovedLoan> ApprovedLoans { get; set; }
        public DbSet<PaymentInstallment> PaymentInstallments { get; set; }  
        


        public AplicationDbContext()
        {
        }

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TermIRConfiguration());
            modelBuilder.ApplyConfiguration(new InstallmentConfiguration());
            modelBuilder.ApplyConfiguration(new LoanRequestConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new ApprovedLoanConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentInstallamentConfiguration());
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
