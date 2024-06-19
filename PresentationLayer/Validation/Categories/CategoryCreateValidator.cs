using BusinessLogicLayer.Dtos.Categories;
using FluentValidation;
using PresentationLayer.Validation.Products;

namespace PresentationLayer.Validation.Categories;

public class CategoryCreateValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateValidator()
    {
        RuleFor(category => category.Name)
            .CategoryOrProductName();

        RuleFor(category => category.Products)
            .NotNull().WithMessage("Products list should not be null")
            .ForEach(product => product.SetValidator(new ProductCreateValidator())).When(category => category.Products.Any());
    }
}