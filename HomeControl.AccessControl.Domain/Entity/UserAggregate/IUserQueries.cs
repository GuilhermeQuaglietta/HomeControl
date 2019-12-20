namespace HomeControl.AccessControl.Domain.Users
{
    public interface IUserQueries
    {
        bool LoginUser(string userName, string password);
    }
}
