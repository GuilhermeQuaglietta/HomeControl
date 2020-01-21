using FluentValidation;
using HomeControl.AccessControl.WebApi.Requests.Users;

namespace HomeControl.AccessControl.WebApi.Infrastructure.Validators
{
    public class UserPutRequestValidator : AbstractValidator<UserPutRequest>
    {
        public UserPutRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
