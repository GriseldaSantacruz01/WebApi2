using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ApprovedLoanConfiguration : IEntityTypeConfiguration<ApprovedLoan>
    {
        public void Configure(EntityTypeBuilder<ApprovedLoan> entity)
        {
            entity.HasKey(x => x.ApprovedLoanId);
            entity
                .HasOne(x => x.Loan)
                .WithOne(x => x.ApprovedLoan)
                .HasForeignKey<ApprovedLoan>(x => x.LoanId);
            entity.Property(x => x.InterestRate)
                .IsRequired();
            
        }
    }
}
