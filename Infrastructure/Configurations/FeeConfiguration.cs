

using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class FeeConfiguration : IEntityTypeConfiguration<Fee>
{
    public void Configure(EntityTypeBuilder<Fee> entity)
    {
        entity.HasKey(x => x.FeeId);
        entity.Property(x => x.Amount)
            .IsRequired();
        entity
            .HasOne(x => x.Term)
            .WithMany(x => x.Fees);
        entity
            .HasOne(x => x.Loan)
            .WithOne(x => x.Fee);
            
        

    }
}
