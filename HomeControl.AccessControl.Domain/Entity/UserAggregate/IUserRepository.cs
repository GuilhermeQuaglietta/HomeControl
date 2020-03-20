using HomeControl.Core.Infrastructure.Repository;

namespace HomeControl.AccessControl.Domain.Users
{
    public interface IUserRepository : IRepository<User>
    {
        string GenerateRecoveryKey(int userId, int expirationSeconds);

        void ChangePassword(int userId, string password);
    }
}
