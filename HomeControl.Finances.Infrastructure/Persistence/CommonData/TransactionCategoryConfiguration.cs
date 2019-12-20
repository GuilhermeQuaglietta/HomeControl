using HomeControl.Finances.Domain.Entity.CommonAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeControl.Finances.Infrastructure.Persistence
{
    public class TransactionCategoryConfiguration : IEntityTypeConfiguration<TransactionCategoryEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionCategoryEntity> builder)
        {
            builder.ToTable("TransactionCategory");
            builder.HasKey(x => x.TransactionCategoryId);

            builder.Property(x => x.Title).IsRequired();
        }
    }
}
