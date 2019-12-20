using HomeControl.Core.Infrastructure.Implementation;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Repository
{
    public class AccountTransferRepository : BaseRepository<AccountTransferEntity>, IAccountTransferRepository
    {
        public AccountTransferRepository(AccountDbContext dbContext)
            : base(dbContext)
        {
            
        }
    }
}
