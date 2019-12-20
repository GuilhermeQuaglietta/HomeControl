using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            builder.ToTable("Account");
            builder.HasKey(x => x.AccountId);
            builder.Property(x => x.AccountId).UseSqlServerIdentityColumn();
        }
    }
}
