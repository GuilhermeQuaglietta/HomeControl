using HomeControl.AccessControl.Domain.Users;
using HomeControl.AccessControl.Infrastructure.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeControl.AccessControl.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AccessControlDbContext _dbContext;

        public UserRepository(AccessControlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Get(object id)
        {
            return _dbContext.Users.Find(id);
        }

        public IEnumerable<User> GetDuplicates(User entity)
        {
            return _dbContext.Users.Where(x => x.UserId != entity.UserId && (
                x.Email.Equals(entity.Email, StringComparison.CurrentCultureIgnoreCase) ||
                x.UserName.Equals(entity.UserName, StringComparison.CurrentCultureIgnoreCase)))
                .AsNoTracking().
                ToList();
        }

        public void Insert(User entity)
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(User entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }

        public User Find(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
