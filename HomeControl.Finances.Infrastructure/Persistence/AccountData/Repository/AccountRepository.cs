using HomeControl.Core.Infrastructure.Repository;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Repository
{
    public class AccountRepository : EntityRepository<AccountEntity, AccountDbContext>, IAccountRepository
    {
        public AccountRepository(AccountDbContext dbContext)
            : base(dbContext)
        {
            
        }
    }
}
