using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeControl.Finances.Infrastructure.Persistence.Contract
{
    class ContractConfiguration : IEntityTypeConfiguration<ContractEntity>
    {
        public void Configure(EntityTypeBuilder<ContractEntity> builder)
        {
            builder.ToTable("Contract");
            builder.HasKey(x => x.ContractId);
        }
    }
}
