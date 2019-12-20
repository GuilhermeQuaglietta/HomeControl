using HomeControl.Finances.Domain.Entity.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeControl.Finances.Infrastructure.Persistence
{
    public class BrandConfiguration : IEntityTypeConfiguration<BrandEntity>
    {
        public void Configure(EntityTypeBuilder<BrandEntity> builder)
        {
            builder.ToTable("Brand");
            builder.HasKey(x => x.Id);
        }
    }
}
