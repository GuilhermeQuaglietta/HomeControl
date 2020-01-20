using HomeControl.AccessControl.Domain.Users;
using HomeControl.AccessControl.Infrastructure.Seedwork;
using HomeControl.Core.Infrastructure.Implementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeControl.AccessControl.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly AccessControlDbContext _dbContext;

        public UserRepository(AccessControlDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override IEnumerable<User> GetDuplicates(User entity)
        {
            return _dbContext.Users.Where(x => x.UserId != entity.UserId &&
                x.Email.Equals(entity.Email, StringComparison.CurrentCultureIgnoreCase))
                .AsNoTracking().
                ToList();
        }

        public string GenerateRecoveryKey(int userId, int expirationSeconds)
        {
            var user = Find(userId);
            user.RecoveryKey = Guid.NewGuid().ToString();
            user.RecoveryExpiration = DateTime.Now.AddSeconds(expirationSeconds);

            Update(user);
            return user.RecoveryKey;
        }

        public void ChangePassword(int userId, string password)
        {
            var user = _dbContext.Users.Find(userId);
            user.Password = password;
            user.RecoveryKey = null;
            user.RecoveryExpiration = null;
            Update(user);
        }
    }
}
