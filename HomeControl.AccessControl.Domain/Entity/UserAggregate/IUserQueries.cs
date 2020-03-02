namespace HomeControl.AccessControl.Domain.Users
{
    public interface IUserQueries
    {
        User LoginUser(string email, string password);
        User FindByEmail(string email);
        User FindByRecoveryKey(string recoveryKey);
    }
}
