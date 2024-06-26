using FluentValidation;
using BusinessLogicLayer.Dtos.Users;

namespace BusinessLogicLayer.Validation.Users;

public class UserLoginValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginValidator()
    {
        RuleFor(user => user.UserName)
            .UserName();

        RuleFor(user => user.Password)
            .Password();
    }
}
