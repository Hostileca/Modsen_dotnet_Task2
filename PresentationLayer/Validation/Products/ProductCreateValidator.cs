using BusinessLogicLayer.Dtos.Products;
using FluentValidation;

namespace PresentationLayer.Validation.Products;

public class ProductCreateValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateValidator()
    {
        RuleFor(product => product.Name)
            .CategoryOrProductName();

        RuleFor(product => product.Description)
            .ProductDescription();

        RuleFor(product => product.Price)
            .Price();

        RuleFor(product => product.CategoryId)
            .IsGuid();
    }
}