using HomeControl.Finances.Domain.Entity.AccountAggregate;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity
{
    public class AccountTransferEntity : AccountTransfer
    {
        public object GetId()
        {
            return AccountTransferId;
        }
    }
}
