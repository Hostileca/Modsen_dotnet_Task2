using AutoMapper;
using BusinessLogicLayer.Dtos.OrderItems;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services.Implementations
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderItemService(IOrderItemRepository orderItemRepository, IProductRepository productRepository,
            IOrderRepository orderRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OrderItemReadDto> CreateOrderItemAsync(OrderItemCreateDto orderItemCreateDto, CancellationToken cancellationToken = default)
        {
            if (orderItemCreateDto == null)
            {
                throw new ArgumentNullException(nameof(orderItemCreateDto));
            }

            var orderItem = _mapper.Map<OrderItem>(orderItemCreateDto);

            var existingProduct = await _productRepository.GetByIdAsync(orderItemCreateDto.ProductId, cancellationToken);
            if (existingProduct == null)
            {
                throw new NotFoundException($"Product not found with id: {orderItemCreateDto.ProductId}");
            }

            var existingOrder = await _orderRepository.GetByIdAsync(orderItemCreateDto.OrderId, cancellationToken);
            if (existingOrder == null)
            {
                throw new NotFoundException($"Order not found with id: {orderItemCreateDto.OrderId}");
            }

            orderItem.Product = existingProduct;
            orderItem.Order = existingOrder;

            await _orderItemRepository.AddAsync(orderItem, cancellationToken);
            await _orderItemRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<OrderItemReadDto>(orderItem);
        }

        public async Task<OrderItemReadDto> DeleteOrderItemByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id, cancellationToken);
            if (orderItem == null)
            {
                throw new NotFoundException($"Order item not found with id: {id}");
            }

            _orderItemRepository.Delete(orderItem);
            await _orderItemRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<OrderItemReadDto>(orderItem);
        }

        public async Task<IEnumerable<OrderItemReadDto>> GetAllOrderItemsAsync(CancellationToken cancellationToken = default)
        {
            var orderItems = await _orderItemRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<OrderItemReadDto>>(orderItems);
        }

        public async Task<OrderItemReadDto> GetOrderItemByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id, cancellationToken);
            if (orderItem == null)
            {
                throw new KeyNotFoundException($"Order item not found with id: {id}");
            }

            return _mapper.Map<OrderItemReadDto>(orderItem);
        }

        public async Task<OrderItemReadDto> UpdateOrderItemAsync(OrderItemUpdateDto orderItemUpdateDto, CancellationToken cancellationToken = default)
        {
            if (orderItemUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(orderItemUpdateDto));
            }

            var existingOrderItem = await _orderItemRepository.GetByIdAsync(orderItemUpdateDto.Id, cancellationToken);
            if (existingOrderItem == null)
            {
                throw new KeyNotFoundException($"Order item not found with id: {orderItemUpdateDto.Id}");
            }

            var newOrderItem = _mapper.Map(orderItemUpdateDto, existingOrderItem);
            await _orderItemRepository.SaveChangesAsync(cancellationToken);
            return _mapper.Map<OrderItemReadDto>(newOrderItem);
        }
    }
}
