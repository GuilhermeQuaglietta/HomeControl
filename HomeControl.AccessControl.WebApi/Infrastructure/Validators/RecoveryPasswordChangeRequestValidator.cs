using FluentValidation;
using HomeControl.AccessControl.WebApi.Requests.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeControl.AccessControl.WebApi.Infrastructure.Validators
{
    public class RecoveryPasswordChangeRequestValidator: AbstractValidator<RecoveryPasswordChangeRequest>
    {

        public RecoveryPasswordChangeRequestValidator()
        {
            RuleFor(x => x.Recoverykey).NotNull().NotEmpty();
            RuleFor(x => x.NewPassword).NotNull().NotEmpty();
            RuleFor(x => x.NewPasswordConfirmation).NotNull().NotEmpty();

            RuleFor(x => x.NewPassword).Matches(x => x.NewPasswordConfirmation).WithMessage("Passwords must math");
        }
    }
}
