using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Configuration
{
    public class AccountTransferConfiguration : IEntityTypeConfiguration<AccountTransferEntity>
    {
        public void Configure(EntityTypeBuilder<AccountTransferEntity> builder)
        {
            builder.ToTable("AccountTransfer");
            builder.HasKey(x => x.AccountTransferId);
        }
    }
}
