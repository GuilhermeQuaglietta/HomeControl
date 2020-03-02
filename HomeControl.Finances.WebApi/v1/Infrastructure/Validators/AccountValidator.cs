using FluentValidation;

namespace HomeControl.Finances.WebApi.v1.Message.AccountMessage
{
    public class AccountValidator : AbstractValidator<AccountRequest>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}
