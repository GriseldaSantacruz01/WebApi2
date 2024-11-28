using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PaymentInstallamentConfiguration : IEntityTypeConfiguration<PaymentInstallment>
{
    public void Configure(EntityTypeBuilder<PaymentInstallment> entity)
    {
        entity.HasKey(x => x.PaymentInstallmentId);
        entity
            .HasOne(x => x.Installment)
            .WithOne(x => x.PaymentInstallment)
            .HasForeignKey<PaymentInstallment>(x => x.InstallmentId);
    }
}
