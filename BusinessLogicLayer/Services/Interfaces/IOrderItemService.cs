using BusinessLogicLayer.Dtos.OrderItems;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemReadDto>> GetAllOrderItemsAsync();
        Task<OrderItemReadDto> GetOrderItemByIdAsync(Guid id);
        Task<OrderItemReadDto> CreateOrderItemAsync(OrderItemCreateDto OrderItemCreateDto);
        Task<OrderItemReadDto> DeleteOrderItemByIdAsync(Guid id);
        Task<OrderItemReadDto> UpdateOrderItemAsync(OrderItemUpdateDto orderItemUpdateDto);
    }
}
