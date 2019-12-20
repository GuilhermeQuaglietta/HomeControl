using System.Collections.Generic;

namespace HomeControl.Core.Infrastructure.Contract
{
    public interface IRepository<TEntity> : IBaseInfrastructure
        where TEntity : class
    {
        TEntity Get(object id);
        TEntity Find(object id);
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetDuplicates(TEntity entity);

        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        void Delete(object id);
    }
}
