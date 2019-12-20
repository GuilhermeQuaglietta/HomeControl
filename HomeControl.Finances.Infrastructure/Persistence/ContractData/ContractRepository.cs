using HomeControl.Core.Infrastructure.Implementation;

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
