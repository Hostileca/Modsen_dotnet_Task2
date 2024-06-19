using BusinessLogicLayer.Dtos.Orders;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync();
        Task<OrderReadDto> GetOrderByIdAsync(Guid id);
        Task<OrderReadDto> GetOrderByPredicateAsync(Expression<Func<OrderReadDto, bool>> predicate);
        Task<OrderReadDto> CreateOrderAsync(OrderCreateDto OrderCreateDto);
        Task<OrderReadDto> DeleteOrderByIdAsync(Guid id);
    }
}
