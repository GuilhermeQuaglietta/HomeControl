using HomeControl.Finances.Domain.Entity.CardAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeControl.Finances.Infrastructure.Persistence
{
    public class CardConfiguration : IEntityTypeConfiguration<CardEntity>
    {
        public void Configure(EntityTypeBuilder<CardEntity> builder)
        {
            builder.ToTable("Card");
            builder.HasKey(x => x.CardId);

            builder.Property(x => x.Title).IsRequired();
        }
        
    }
}
