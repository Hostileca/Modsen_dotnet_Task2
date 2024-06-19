using BusinessLogicLayer.Dtos.Categories;
using FluentValidation;

namespace PresentationLayer.Validation.Categories;

public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateValidator()
    {
        RuleFor(category => category.Name)
            .CategoryOrProductName();
    }
}
