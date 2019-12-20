using HomeControl.Core.Infrastructure.Contract;
using HomeControl.Finances.Domain.Entity.AccountAggregate;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity
{
    public class AccountEntity : Account, IEntity
    {
        public object GetId()
        {
            return AccountId;
        }
    }
}
