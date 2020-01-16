namespace HomeControl.AccessControl.Domain.Users
{
    public interface IUserQueries
    {
        bool LoginUser(string userName, string password);
        User FindByEmail(string email);
        User FindByRecoveryKey(string recoveryKey);
    }
}
