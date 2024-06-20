using BusinessLogicLayer.Dtos.Users;
using FluentValidation;

namespace BusinessLogicLayer.Validation.Users;

public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateValidator()
    {
        RuleFor(user => user.Id)
            .IsGuid();

        RuleFor(user => user.UserName)
            .UserName();

        RuleFor(user => user.Password)
            .Password();

        RuleFor(user => user.RoleId)
            .IsGuid();
    }
}