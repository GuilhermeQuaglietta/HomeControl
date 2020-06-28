using HomeControl.Finances.Infrastructure.Persistence.AccountData.Configuration;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData
{
    public class AccountDbContext : DbContext
    {
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<AccountTransactionEntity> Transactions { get; set; }
        public DbSet<AccountTransferEntity> Transfers { get; set; }

        public AccountDbContext(DbContextOptions options)
            : base(options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new AccountTypeConfiguration());

            modelBuilder.ApplyConfiguration(new AccountTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new AccountTransferConfiguration());
        }
    }
}
