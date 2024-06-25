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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OrderReadDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            if (orderCreateDto == null)
                throw new ArgumentNullException(nameof(orderCreateDto));

            var orderRepository = _unitOfWork.GetRepository<Order>();

            var order = _mapper.Map<Order>(orderCreateDto);

            await orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task<OrderReadDto> DeleteOrderByIdAsync(Guid id)
        {
            var orderRepository = _unitOfWork.GetRepository<Order>();

            var order = await orderRepository.GetByIdAsync(id);
            if (order == null)
                throw new NotFoundException($"Order not found with id: {id}");

            orderRepository.Delete(order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync()
        {
            var orderRepository = _unitOfWork.GetRepository<Order>();

            var orders = await orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderReadDto>>(orders);
        }

        public async Task<OrderReadDto> GetOrderByIdAsync(Guid id)
        {
            var orderRepository = _unitOfWork.GetRepository<Order>();

            var order = await orderRepository.GetByIdAsync(id);
            if (order == null)
                throw new NotFoundException($"Order not found with id: {id}");

            return _mapper.Map<OrderReadDto>(order);
        }
    }
}