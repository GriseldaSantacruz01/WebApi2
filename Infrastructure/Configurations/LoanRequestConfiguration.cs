using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class LoanRequestConfiguration : IEntityTypeConfiguration<LoanRequest>
    {
        public void Configure(EntityTypeBuilder<LoanRequest> entity)
        {
            entity.HasKey(x => x.LoanId);
            entity.Property(x => x.Type)
                .IsRequired();
            entity
                .HasOne(x => x.ApprovedLoan)
                .WithOne(x => x.Loan)
                .HasForeignKey<LoanRequest>(x => x.ApprovedLoanId);
            entity
                .HasOne(x => x.Fee)
                .WithOne(x => x.Loan)
                .HasForeignKey<LoanRequest>(x => x.FeedId);
            entity.Property(x => x.Type)
                .IsRequired();
            entity
                .HasOne(x => x.Customer)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.CustomerId);
        }
    }
}
