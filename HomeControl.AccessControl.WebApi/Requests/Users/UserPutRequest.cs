namespace HomeControl.AccessControl.WebApi.Requests.Users
{
    public class UserPutRequest
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
