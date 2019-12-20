using HomeControl.Core.Infrastructure.Contract;
using HomeControl.Finances.Domain.Entity.AccountAggregate;
using System;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity
{
    public class AccountTransferEntity : AccountTransfer, IEntity
    {
        public object GetId()
        {
            return AccountTransferId;
        }
    }
}
