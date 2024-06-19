using BusinessLogicLayer.Dtos.Products;
using FluentValidation;

namespace PresentationLayer.Validation.Products;

public class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateValidator()
    {
        RuleFor(product => product.Name)
            .NotNull().WithMessage("Product name should not be null")
            .NotEmpty().WithMessage("Product name should not be empty")
            .Length(5, 50).WithMessage("Product name should have length between 5 and 50");

        RuleFor(product => product.Description)
            .NotNull().WithMessage("Product description should not be null")
            .NotEmpty().WithMessage("Product description should not be empty")
            .MaximumLength(500).WithMessage("Product description should not exceed 500 characters");

        RuleFor(product => product.Price)
            .NotNull().WithMessage("Price should not be null")
            .GreaterThan(0).WithMessage("Product price should be greater than 0");

        RuleFor(product => product.CategoryId)
            .NotNull().WithMessage("Category ID should not be null")
            .NotEmpty().WithMessage("Category ID should not be empty");
    }
}