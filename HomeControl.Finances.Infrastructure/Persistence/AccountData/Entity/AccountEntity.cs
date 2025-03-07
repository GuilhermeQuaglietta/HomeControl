﻿using HomeControl.Core.Infrastructure.Repository;
using HomeControl.Finances.Domain.Entity.AccountAggregate;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity
{
    public class AccountEntity : Account, IEntity
    {
        public int Id { get => AccountId; }
    }
}
