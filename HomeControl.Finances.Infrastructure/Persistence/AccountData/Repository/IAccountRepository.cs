﻿using HomeControl.Core.Infrastructure.Repository;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Repository
{
    public interface IAccountRepository : IEntityRepository<AccountEntity, AccountDbContext>
    {
    }
}
