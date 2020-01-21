using FluentValidation;
using HomeControl.AccessControl.WebApi.Requests.Users;

namespace HomeControl.AccessControl.WebApi.Infrastructure.Validators
{
    public class UserPostRequestValidator : AbstractValidator<UserPostRequest>
    {
        public UserPostRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().Matches(x => x.PasswordConfirmation);
            RuleFor(x => x.PasswordConfirmation).NotEmpty();
        }
    }
}
