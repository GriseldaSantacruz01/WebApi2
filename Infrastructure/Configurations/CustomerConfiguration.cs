using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.HasKey(x => x.CustomerId);
        entity.Property(x => x.FirstName).IsRequired();
        entity.Property(x =>x.LastName).IsRequired();
        entity
            .HasMany(x => x.Loans)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId);
    }
}
