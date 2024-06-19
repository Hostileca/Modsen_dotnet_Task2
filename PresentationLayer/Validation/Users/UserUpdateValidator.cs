using BusinessLogicLayer.Dtos.Users;
using FluentValidation;

namespace PresentationLayer.Validation.Users;

public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateValidator()
    {
        RuleFor(user => user.UserName)
            .NotNull().WithMessage("Username should not be null")
            .NotEmpty().WithMessage("Username should not be empty")
            .Length(3, 20).WithMessage("Username should have length between 3 and 20");

        RuleFor(user => user.Password)
            .NotNull().WithMessage("Password should not be null")
            .NotEmpty().WithMessage("Password should not be empty")
            .Length(8, 20).WithMessage("Password should have length between 8 and 20")
            .Matches("[A-Z]").WithMessage("Password should contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Password should contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Password should contain at least one digit")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password should contain at least one special character.");
    }
}
