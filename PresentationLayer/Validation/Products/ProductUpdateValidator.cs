﻿using BusinessLogicLayer.Dtos.Products;
using FluentValidation;

namespace PresentationLayer.Validation.Products;

public class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateValidator()
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