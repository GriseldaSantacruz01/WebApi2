

using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class InstallmentConfiguration : IEntityTypeConfiguration<Installment>
{
    public void Configure(EntityTypeBuilder<Installment> entity)
    {
        entity.HasKey(x => x.InstallmentId);
        entity
            .HasOne(x => x.ApprovedLoan)
            .WithMany(X => X.Installaments)
            .HasForeignKey(x => x.ApprovedLoanId);
        entity.Property(x => x.PaymentDate)
            .IsRequired(false)
            .HasDefaultValue(null);
    }
}
