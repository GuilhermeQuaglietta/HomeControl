using HomeControl.Core.Infrastructure.Repository;

namespace HomeControl.Finances.Infrastructure.Persistence.Contract
{
    public class ContractRepository : BaseRepository<ContractEntity>, IContractRepository
    {
        public ContractRepository(ContractDbContext context) 
            : base(context)
        {
        }
    }
}
