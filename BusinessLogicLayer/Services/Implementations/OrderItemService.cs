using AutoMapper;
using BusinessLogicLayer.Dtos.OrderItems;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services.Implementations
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;

        public OrderItemService(IRepository<OrderItem> orderItemRepository, IRepository<Product> productRepository, IRepository<Order> orderRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OrderItemReadDto> CreateOrderItemAsync(OrderItemCreateDto orderItemCreateDto)
        {
            if (orderItemCreateDto == null)
                throw new ArgumentNullException(nameof(orderItemCreateDto));
            
            var orderItem = _mapper.Map<OrderItem>(orderItemCreateDto);

            var existingProduct = await _productRepository.GetByIdAsync(orderItemCreateDto.ProductId);
            if (existingProduct == null)
                throw new KeyNotFoundException($"Product not found with id: {orderItemCreateDto.ProductId}");

            var existingOrder = await _orderRepository.GetByIdAsync(orderItemCreateDto.OrderId);
            if (existingOrder == null)
                throw new KeyNotFoundException($"Order not found with id: {orderItemCreateDto.OrderId}");

            orderItem.Product = existingProduct;
            orderItem.Order = existingOrder;

            await _orderItemRepository.AddAsync(orderItem);
            await _orderItemRepository.SaveChangesAsync();

            return _mapper.Map<OrderItemReadDto>(orderItem);
        }

        public async Task<OrderItemReadDto> DeleteOrderItemByIdAsync(Guid id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);
            if (orderItem == null)
                throw new KeyNotFoundException($"Order item not found with id: {id}");

            _orderItemRepository.Delete(orderItem);
            await _orderItemRepository.SaveChangesAsync();

            return _mapper.Map<OrderItemReadDto>(orderItem);
        }

        public async Task<IEnumerable<OrderItemReadDto>> GetAllOrderItemsAsync()
        {
            var orderItems = await _orderItemRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderItemReadDto>>(orderItems);
        }

        public async Task<OrderItemReadDto> GetOrderItemByIdAsync(Guid id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);
            if (orderItem == null)
                throw new KeyNotFoundException($"Order item not found with id: {id}");

            return _mapper.Map<OrderItemReadDto>(orderItem);
        }

        public async Task<IEnumerable<OrderItemReadDto>> GetOrderItemByPredicateAsync(Expression<Func<OrderItemReadDto, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var orderItemPredicate = _mapper.Map<Expression<Func<OrderItem, bool>>>(predicate);
            var orderItems = await _orderItemRepository.GetByPredicateAsync(orderItemPredicate);
            if (orderItems == null)
                throw new KeyNotFoundException("Order item not found.");

            return _mapper.Map<IEnumerable<OrderItemReadDto>>(orderItems);
        }
    }
}
