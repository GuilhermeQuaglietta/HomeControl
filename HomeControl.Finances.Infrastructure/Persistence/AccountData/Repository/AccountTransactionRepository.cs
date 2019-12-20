using HomeControl.Core.Infrastructure.Implementation;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Repository
{
    public class AccountTransactionRepository : BaseRepository<AccountTransactionEntity>, IAccountTransactionRepository
    {
        public AccountTransactionRepository(AccountDbContext dbContext)
            : base(dbContext)
        {
            
        }
    }
}
