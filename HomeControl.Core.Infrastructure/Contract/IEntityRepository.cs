using Microsoft.EntityFrameworkCore;

namespace HomeControl.Core.Infrastructure.Contract
{
    public interface IEntityRepository<TEntity, TDbContext> : IRepository<TEntity> 
        where TEntity : class
        where TDbContext : DbContext
    {
    }
}
