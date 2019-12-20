using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeControl.Finances.Infrastructure.Persistence.Contract
{
    public class ContractItemConfiguration : IEntityTypeConfiguration<ContractItemEntity>
    {
        public void Configure(EntityTypeBuilder<ContractItemEntity> builder)
        {
            builder.ToTable("ContractItem");
            builder.HasKey(x => x.ContractItemId);
        }
    }
}
