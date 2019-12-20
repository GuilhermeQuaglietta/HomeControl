using HomeControl.Core.Infrastructure.Contract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;

namespace HomeControl.Core.Infrastructure.Implementation
{
    public class EntityRepository<TEntity, TDbContext> : IRepository<TEntity>
        where TEntity : class
        where TDbContext : DbContext
    {
        protected readonly DbContext _dbContext;
        protected readonly IDbConnection _dbConnection;

        public EntityRepository(IDbConnection dbConnection, TDbContext dbContext)
        {
            _dbConnection = dbConnection;
            _dbContext = dbContext;
        }

        public void Delete(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public TEntity Find(object id)
        {
            throw new System.NotImplementedException();
        }

        public TEntity Get(object id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TEntity> GetDuplicates(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            throw new System.NotImplementedException();
        }
    }
}
