﻿using HomeControl.AccessControl.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeControl.AccessControl.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.UserId);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.RecoveryKey);
            builder.Property(x => x.RecoveryExpiration);
            builder.Property(x => x.RecoveryAnswer);

            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
