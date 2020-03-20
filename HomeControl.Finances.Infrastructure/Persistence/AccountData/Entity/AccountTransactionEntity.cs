using HomeControl.Core.Infrastructure.Repository;
using HomeControl.Finances.Domain.Entity.AccountAggregate;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity
{
    public class AccountTransactionEntity : AccountTransaction, IEntity
    {
        public object GetId()
        {
            return AccountTransactionId;
        }
    }
}
