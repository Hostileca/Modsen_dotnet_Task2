using BusinessLogicLayer.Dtos.OrderItems;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/v1/order_items")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems(CancellationToken cancellationToken = default)
        {
            var orderItems = await _orderItemService.GetAllOrderItemsAsync(cancellationToken);
            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItemById(Guid id, CancellationToken cancellationToken = default)
        {
            var orderItem = await _orderItemService.GetOrderItemByIdAsync(id, cancellationToken);
            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem(OrderItemCreateDto orderItemCreateDto, CancellationToken cancellationToken = default)
        {
            var orderItem = await _orderItemService.CreateOrderItemAsync(orderItemCreateDto, cancellationToken);
            return Ok(orderItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(Guid id, OrderItemUpdateDto orderItemUpdateDto, CancellationToken cancellationToken = default)
        {
            orderItemUpdateDto.Id = id;
            var orderItem = await _orderItemService.UpdateOrderItemAsync(orderItemUpdateDto, cancellationToken);
            return Ok(orderItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(Guid id, CancellationToken cancellationToken = default)
        {
            var deletedOrderItem = await _orderItemService.DeleteOrderItemByIdAsync(id, cancellationToken);
            return Ok(deletedOrderItem);
        }
    }
}
