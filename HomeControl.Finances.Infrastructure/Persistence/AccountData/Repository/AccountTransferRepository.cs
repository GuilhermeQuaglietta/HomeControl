﻿using HomeControl.Core.Infrastructure.Repository;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Repository
{
    public class AccountTransferRepository : EntityRepository<AccountTransferEntity, AccountDbContext>, IAccountTransferRepository
    {
        public AccountTransferRepository(AccountDbContext dbContext)
            : base(dbContext)
        {
            
        }
    }
}
