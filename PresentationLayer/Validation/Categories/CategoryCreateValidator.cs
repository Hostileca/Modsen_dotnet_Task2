using BusinessLogicLayer.Dtos.Categories;
using FluentValidation;
using PresentationLayer.Validation.Products;

namespace PresentationLayer.Validation.Categories;

public class CategoryCreateValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateValidator()
    {
        RuleFor(category => category.Name)
            .NotNull().WithMessage("Category name should not be null")
            .NotEmpty().WithMessage("Category name should not be empty")
            .Length(2, 50).WithMessage("Product name should have length between 2 and 50");

        RuleFor(category => category.Products)
            .NotNull().WithMessage("Category name should not be null")
            .ForEach(product => product.SetValidator(new ProductCreateValidator())).When(category => category.Products.Any());
    }
}