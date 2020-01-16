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

        public bool LoginUser(string userName, string password)
        {
            return _dbContext.Users.Any(x => x.Email.Equals(userName, StringComparison.CurrentCultureIgnoreCase) && x.Password == password);
        }

        public User FindByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
        } 

        public User FindByRecoveryKey(string recoveryKey)
        {
            return _dbContext.Users.FirstOrDefault(x =>
                x.RecoveryKey.Equals(recoveryKey, StringComparison.CurrentCultureIgnoreCase) &&
                x.RecoveryExpiration >= DateTime.Now
            );
        }
    }
}
