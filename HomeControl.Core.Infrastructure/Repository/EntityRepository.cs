using HomeControl.Core.Infrastructure.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HomeControl.Core.Infrastructure.Implementation
{
    public class EntityRepository<TEntity, TDbContext> : BaseRepository<TEntity>, IEntityRepository<TEntity, TDbContext>
        where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        protected readonly DbContext _dbContext;
        protected readonly IDbConnection _dbConnection;

        public EntityRepository(TDbContext dbContext)
            : base(dbContext)
        {
            _dbConnection = dbContext.Database.GetDbConnection();
            _dbContext = dbContext;
        }

        public TEntity Get(int id, int ownerId)
        {
            var entity = dbContext.Set<TEntity>().Find(id);
            CheckOwner(entity, ownerId);

            return entity;
        }
        public IEnumerable<TEntity> GetAll(int ownerId)
        {
            return dbContext.Set<TEntity>().AsNoTracking().Where(x => x.OwnerId == ownerId);
        }
        public TEntity Find(int id, int ownerId)
        {
            var entity = dbContext.Set<TEntity>().Find(id);
            CheckOwner(entity, ownerId);
            return entity;
        }

        public virtual void Insert(TEntity entity, int ownerId)
        {
            CheckOwner(entity, ownerId);

            dbContext.Add(entity);
            dbContext.SaveChanges();
        }
        public virtual void Insert(IEnumerable<TEntity> entities, int ownerId)
        {
            CheckOwner(entities, ownerId);

            dbContext.AddRange(entities);
            dbContext.SaveChanges();
        }

        public virtual void Update(TEntity entity, int ownerId)
        {
            CheckOwner(entity, ownerId);

            dbContext.Update(entity);
            dbContext.SaveChanges();
        }
        public virtual void Update(IEnumerable<TEntity> entities, int ownerId)
        {
            CheckOwner(entities, ownerId);

            dbContext.UpdateRange(entities);
            dbContext.SaveChanges();
        }

        public virtual void Delete(TEntity entity, int ownerId)
        {
            CheckOwner(entity, ownerId);

            dbContext.Remove(entity);
            dbContext.SaveChanges();
        }
        public virtual void Delete(IEnumerable<TEntity> entities, int ownerId)
        {
            CheckOwner(entities, ownerId);

            dbContext.RemoveRange(entities);
            dbContext.SaveChanges();
        }

        public void Delete(int id, int ownerId)
        {
            var entity = Find(id, ownerId);

            if (entity == null)
                throw new InvalidOperationException("Entity not found");

            CheckOwner(entity, ownerId);

            Delete(entity, ownerId);
        }

        protected virtual void CheckOwner(TEntity entity, int ownerId)
        {
            if (entity.OwnerId != ownerId)
                throw new EntityDifferentOwnerException("OwnerId is different from the current loggedIn user");
        }

        protected virtual void CheckOwner(IEnumerable<TEntity> entities, int ownerId)
        {
            if (entities.Any(x => x.OwnerId != ownerId))
                throw new EntityDifferentOwnerException("OwnerId from an entity in the list is different from the current loggedIn user");
        }
    }
}
