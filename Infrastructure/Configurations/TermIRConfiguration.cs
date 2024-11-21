using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class TermIRConfiguration : IEntityTypeConfiguration<TermIR>
    {
        public void Configure(EntityTypeBuilder<TermIR> entity)
        {
            entity.HasKey(x => x.TermId);
            entity.Property(x => x.Months)
                .IsRequired();
            entity.Property(x => x.InterestRate)
                .IsRequired();
        }
    }
}
