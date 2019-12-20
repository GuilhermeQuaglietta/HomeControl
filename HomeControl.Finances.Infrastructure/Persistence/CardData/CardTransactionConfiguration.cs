using HomeControl.Finances.Domain.Entity.CardAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeControl.Finances.Infrastructure.Persistence
{
    public class CardTransactionConfiguration : IEntityTypeConfiguration<CardTransactionEntity>
    {
        public void Configure(EntityTypeBuilder<CardTransactionEntity> builder)
        {
            builder.ToTable("CardTransaction");
            builder.HasKey(x => x.CardTransactionId);
        }
    }
}
