using HomeControl.Core.Infrastructure.Contract;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Repository
{
    public interface IAccountTransferRepository : IRepository<AccountTransferEntity>
    {
    }
}
