using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                .HasOne(x => x.Customer)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.CustomerId);
            entity
                .HasOne(x => x.Term)
                .WithMany(x => x.LoanRequests)
                .HasForeignKey(x => x.Months)
                .HasPrincipalKey(x => x.Months);
        }
    }
}
