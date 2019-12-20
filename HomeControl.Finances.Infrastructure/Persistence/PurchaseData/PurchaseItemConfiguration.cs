using HomeControl.Finances.Domain.Entity.PurchaseAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeControl.Finances.Infrastructure.Persistence
{
    public class PurchaseItemConfiguration : IEntityTypeConfiguration<PurchaseItemEntity>
    {
        public void Configure(EntityTypeBuilder<PurchaseItemEntity> builder)
        {
            builder.ToTable("PurchaseItem");
            builder.HasKey(x => x.PurchaseId);

            builder.Property(x => x.PurchaseId).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.TotalValue).IsRequired();
        }
    }
}
