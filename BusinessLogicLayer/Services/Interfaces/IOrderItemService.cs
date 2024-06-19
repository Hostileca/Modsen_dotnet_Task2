using BusinessLogicLayer.Dtos.OrderItems;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemReadDto>> GetAllOrderItemsAsync();
        Task<OrderItemReadDto> GetOrderItemByIdAsync(Guid id);
        Task<OrderItemReadDto> GetOrderItemByPredicateAsync(Expression<Func<OrderItemReadDto, bool>> predicate);
        Task<OrderItemReadDto> CreateOrderItemAsync(OrderItemCreateDto OrderItemCreateDto);
        Task<OrderItemReadDto> DeleteOrderItemByIdAsync(Guid id);
    }
}
