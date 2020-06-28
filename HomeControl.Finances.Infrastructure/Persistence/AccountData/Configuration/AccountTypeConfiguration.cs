using HomeControl.Finances.Infrastructure.AccountData.Generator;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Configuration
{
    public class AccountTypeConfiguration : IEntityTypeConfiguration<AccountTypeEntity>
    {
        public void Configure(EntityTypeBuilder<AccountTypeEntity> builder)
        {
            builder.ToTable("AccountType");
            builder.HasKey(x => x.AccountTypeId);

            builder.Property(x => x.IncludedDate)
                .HasValueGenerator<DateTimeGenerator>()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.AlteredDate)
                .HasValueGenerator<DateTimeGenerator>()
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}