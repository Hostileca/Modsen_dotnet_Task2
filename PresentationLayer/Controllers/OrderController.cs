using BusinessLogicLayer.Dtos.Orders;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/v1/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken = default)
        {
            var orders = await _orderService.GetAllOrdersAsync(cancellationToken);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id, CancellationToken cancellationToken = default)
        {
            var order = await _orderService.GetOrderByIdAsync(id, cancellationToken);
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateDto orderCreateDto, CancellationToken cancellationToken = default)
        {
            var order = await _orderService.CreateOrderAsync(orderCreateDto, cancellationToken);
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id, CancellationToken cancellationToken = default)
        {
            var deletedOrder = await _orderService.DeleteOrderByIdAsync(id, cancellationToken);
            return Ok(deletedOrder);
        }
    }
}
