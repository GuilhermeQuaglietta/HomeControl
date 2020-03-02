using HomeControl.Core.Infrastructure.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeControl.Core.Infrastructure.Implementation
{
    public class BaseRepository<TEntity> : BaseInfrastructure, IRepository<TEntity>
        where TEntity : class
    { 
        protected readonly DbContext dbContext;

        public BaseRepository(DbContext context)
        {
            dbContext = context;
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public TEntity Get(int id)
        {
            var entity = dbContext.Set<TEntity>().Find(id);
            return entity;
        }
        public IEnumerable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>().AsNoTracking();
        }
        public TEntity Find(int id)
        {
            var entity = dbContext.Set<TEntity>().Find(id);
            return entity;
        }
        public TEntity FindOwner(int id)
        {
            var entity = dbContext.Set<TEntity>().Find(id);
            return entity;
        }

        public virtual void Insert(TEntity entity)
        {
            dbContext.Add(entity);
            dbContext.SaveChanges();
        }
        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            dbContext.AddRange(entities);
            dbContext.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            dbContext.Update(entity);
            dbContext.SaveChanges();
        }
        public virtual void Update(IEnumerable<TEntity> entities)
        {
            dbContext.UpdateRange(entities);
            dbContext.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            dbContext.Remove(entity);
            dbContext.SaveChanges();
        }
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            dbContext.RemoveRange(entities);
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = FindOwner(id);

            if (entity == null)
                throw new InvalidOperationException("Entity not found");

            Delete(entity);
        }

        public virtual IEnumerable<TEntity> GetDuplicates(TEntity entity)
        {
            return null;
        }
    }
}
