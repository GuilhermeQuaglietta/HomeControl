using HomeControl.Core.Validations;

namespace HomeControl.AccessControl.Domain.Users
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        //public DataValidationCollection Validate()
        //{
        //    DataValidationCollection validations = new DataValidationCollection();

        //    if (string.IsNullOrWhiteSpace(Name))
        //        validations.AddError("Name", "Name is required");

        //    if (string.IsNullOrWhiteSpace(UserName))
        //        validations.AddError("UserName", "userName is required");

        //    if (string.IsNullOrWhiteSpace(Password))
        //        validations.AddError("Password", "Password is required");

        //    if (string.IsNullOrWhiteSpace(Email))
        //        validations.AddError("Email", "Email is required");

        //    return validations;
        //}
    }
}
