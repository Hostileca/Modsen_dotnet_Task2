using BusinessLogicLayer.Dtos.Users;
using FluentValidation;

namespace PresentationLayer.Validation.Users;

public class UserCreateValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateValidator()
    {
        RuleFor(user => user.UserName)
            .UserName();

        RuleFor(user => user.Password)
            .Password();
    }
}