using HomeControl.AccessControl.Domain.Users;
using HomeControl.AccessControl.Infrastructure.Seedwork;
using System;
using System.Linq;

namespace HomeControl.AccessControl.Infrastructure.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly AccessControlDbContext _dbContext;

        public UserQueries(AccessControlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User LoginUser(string email, string password)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
        }

        public User FindByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Email == email);
        }

        public User FindByRecoveryKey(string recoveryKey)
        {
            return _dbContext.Users.FirstOrDefault(x =>
                x.RecoveryKey == recoveryKey &&
                x.RecoveryExpiration >= DateTime.Now
            );
        }
    }
}
