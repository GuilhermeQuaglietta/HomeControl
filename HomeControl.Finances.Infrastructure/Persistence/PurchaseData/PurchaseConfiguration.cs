using HomeControl.Finances.Domain.Entity.PurchaseAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeControl.Finances.Infrastructure.Persistence
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<PurchaseEntity>
    {
        public void Configure(EntityTypeBuilder<PurchaseEntity> builder)
        {
            builder.ToTable("PurchaseEntity");
            builder.HasKey(x => x.PurchaseId);
        }
    }
}
