using AutoMapper;
using BusinessLogicLayer.Dtos.Orders;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IRepository<Order> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OrderReadDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            if (orderCreateDto == null)
                throw new ArgumentNullException(nameof(orderCreateDto));

            var order = _mapper.Map<Order>(orderCreateDto);

            foreach (var orderItemDto in orderCreateDto.OrderItems)
            {
                var orderItem = _mapper.Map<OrderItem>(orderItemDto);
                order.OrderItems.Add(orderItem);
            }

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task<OrderReadDto> DeleteOrderByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                throw new KeyNotFoundException($"Order not found with id: {id}");

            _orderRepository.Delete(order);
            await _orderRepository.SaveChangesAsync();

            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderReadDto>>(orders);
        }

        public async Task<OrderReadDto> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                throw new KeyNotFoundException($"Order not found with id: {id}");

            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task<OrderReadDto> GetOrderByPredicateAsync(Expression<Func<OrderReadDto, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var orderPredicate = _mapper.Map<Expression<Func<Order, bool>>>(predicate);
            var order = await _orderRepository.GetByPredicateAsync(orderPredicate);
            if (order == null)
                throw new KeyNotFoundException("Order not found.");

            return _mapper.Map<OrderReadDto>(order);
        }
    }
}
