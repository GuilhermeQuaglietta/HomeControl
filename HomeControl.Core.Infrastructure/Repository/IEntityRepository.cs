using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HomeControl.Core.Infrastructure.Repository
{
    public interface IEntityRepository<TEntity, TDbContext> : IRepository<TEntity> 
        where TEntity : class
        where TDbContext : DbContext
    {
        TEntity Get(int id, int ownerId);
        TEntity Find(int id, int ownerId);
        IEnumerable<TEntity> GetAll(int ownerId);

        void Insert(TEntity entity, int ownerId);
        void Insert(IEnumerable<TEntity> entities, int ownerId);

        void Update(TEntity entity, int ownerId);
        void Update(IEnumerable<TEntity> entities, int ownerId);

        void Delete(TEntity entity, int ownerId);
        void Delete(IEnumerable<TEntity> entities, int ownerId);
        void Delete(int id, int ownerId);
    }
}
