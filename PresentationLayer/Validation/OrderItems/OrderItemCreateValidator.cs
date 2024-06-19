using BusinessLogicLayer.Dtos.OrderItems;
using FluentValidation;

namespace PresentationLayer.Validation.OrderItems;

public class OrderItemCreateValidator : AbstractValidator<OrderItemCreateDto>
{
    public OrderItemCreateValidator()
    {
        RuleFor(orderItem => orderItem.Amount)
            .NotNull().WithMessage("Amount should not be null")
            .GreaterThan(0).WithMessage("Amount should be greater than 0");

        RuleFor(orderItem => orderItem.ProductId)
            .IsGuid();
    }
}
