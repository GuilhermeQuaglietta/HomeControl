using HomeControl.Core.Infrastructure.Repository;
using HomeControl.Finances.Domain.Entity.AccountAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity
{
    public class AccountTypeEntity : AccountType, IEntity
    {
        public int Id => AccountTypeId;
    }
}
