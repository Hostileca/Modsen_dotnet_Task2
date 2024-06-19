using BusinessLogicLayer.Dtos.OrderItems;
using FluentValidation;

namespace PresentationLayer.Validation.OrderItems;

public class OrderItemCreateValidator : AbstractValidator<OrderItemCreateDto>
{
    public OrderItemCreateValidator()
    {
        RuleFor(orderItem => orderItem.Amount)
            .ItemAmount();

        RuleFor(orderItem => orderItem.ProductId)
            .IsGuid();
    }
}
