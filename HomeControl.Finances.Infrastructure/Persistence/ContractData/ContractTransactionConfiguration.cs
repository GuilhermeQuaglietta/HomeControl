using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeControl.Finances.Infrastructure.Persistence.Contract
{
    public class ContractTransactionConfiguration : IEntityTypeConfiguration<ContractTransactionEntity>
    {
        public void Configure(EntityTypeBuilder<ContractTransactionEntity> builder)
        {
            builder.ToTable("ContractTransaction");
            builder.HasKey(x => x.ContractTransactionId);
        }
    }
}
