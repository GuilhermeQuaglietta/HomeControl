using FluentValidation;
using HomeControl.AccessControl.WebApi.Requests.Login;

namespace HomeControl.AccessControl.WebApi.Infrastructure.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}
