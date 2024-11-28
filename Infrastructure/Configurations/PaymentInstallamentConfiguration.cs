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
            .HasMany(p => p.Installments)
            .WithOne(i => i.PaymentInstallment)
            .HasForeignKey(i => i.PaymentInstallmentId)
            .IsRequired(false);
    }
}
