using AutoMapper;
using BusinessLogicLayer.Dtos.Orders;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderReadDto> CreateOrderAsync(OrderCreateDto orderCreateDto, CancellationToken cancellationToken = default)
        {
            if (orderCreateDto == null)
            {
                throw new ArgumentNullException(nameof(orderCreateDto));
            }

            var order = _mapper.Map<Order>(orderCreateDto);

            await _orderRepository.AddAsync(order, cancellationToken);
            await _orderRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task<OrderReadDto> DeleteOrderByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
            if (order == null)
            {
                throw new NotFoundException($"Order not found with id: {id}");
            }

            _orderRepository.Delete(order);
            await _orderRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
        {
            var orders = await _orderRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<OrderReadDto>>(orders);
        }

        public async Task<OrderReadDto> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
            if (order == null)
            {
                throw new NotFoundException($"Order not found with id: {id}");
            }

            return _mapper.Map<OrderReadDto>(order);
        }
    }
}
