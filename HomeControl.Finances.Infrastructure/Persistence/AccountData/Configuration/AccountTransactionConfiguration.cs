using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Configuration
{
    public class AccountTransactionConfiguration : IEntityTypeConfiguration<AccountTransactionEntity>
    {
        public void Configure(EntityTypeBuilder<AccountTransactionEntity> builder)
        {
            builder.ToTable("AccountTransaction");
            builder.HasKey(x => x.AccountTransactionId);
        }
    }
}
