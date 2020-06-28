using HomeControl.Core.Infrastructure.Repository;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;

namespace HomeControl.Finances.Infrastructure.Persistence.AccountData.Repository
{
    public class AccountTypeRepository : EntityRepository<AccountTypeEntity, AccountDbContext>, IAccountTypeRepository
    {
        public AccountTypeRepository(AccountDbContext dbContext)
            : base(dbContext)
        {
            
        }
    }
}
