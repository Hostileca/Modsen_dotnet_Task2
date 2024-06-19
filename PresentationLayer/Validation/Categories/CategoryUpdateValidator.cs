using BusinessLogicLayer.Dtos.Categories;
using FluentValidation;

namespace PresentationLayer.Validation.Categories;

public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateValidator()
    {
        RuleFor(category => category.Name)
            .NotNull().WithMessage("Category name should not be null")
            .NotEmpty().WithMessage("Category name should not be empty")
            .Length(2, 50).WithMessage("Product name should have length between 2 and 50");
    }
}
