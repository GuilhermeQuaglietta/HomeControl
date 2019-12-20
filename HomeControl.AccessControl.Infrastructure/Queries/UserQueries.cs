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
            return _dbContext.Users.Any(x => x.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase) && x.Password == password);
        }
    }
}
