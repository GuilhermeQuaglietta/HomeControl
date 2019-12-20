using HomeControl.Core.Infrastructure.Implementation;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Repository
{
    public class AccountRepository : BaseRepository<AccountEntity>, IAccountRepository
    {
        public AccountRepository(AccountDbContext dbContext)
            : base(dbContext)
        {
            
        }
    }
}
