using BusinessLogicLayer.Dtos.Orders;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync();
        Task<OrderReadDto> GetOrderByIdAsync(Guid id);
        Task<OrderReadDto> CreateOrderAsync(OrderCreateDto OrderCreateDto);
        Task<OrderReadDto> DeleteOrderByIdAsync(Guid id);
    }
}
