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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OrderItemReadDto> CreateOrderItemAsync(OrderItemCreateDto orderItemCreateDto)
        {
            if (orderItemCreateDto == null)
                throw new ArgumentNullException(nameof(orderItemCreateDto));

            var orderItemRepository = _unitOfWork.GetRepository<OrderItem>();
            var productRepository = _unitOfWork.GetRepository<Product>();
            var orderRepository = _unitOfWork.GetRepository<Order>();

            var orderItem = _mapper.Map<OrderItem>(orderItemCreateDto);

            var existingProduct = await productRepository.GetByIdAsync(orderItemCreateDto.ProductId);
            if (existingProduct == null)
                throw new NotFoundException($"Product not found with id: {orderItemCreateDto.ProductId}");

            var existingOrder = await orderRepository.GetByIdAsync(orderItemCreateDto.OrderId);
            if (existingOrder == null)
                throw new NotFoundException($"Order not found with id: {orderItemCreateDto.OrderId}");

            orderItem.Product = existingProduct;
            orderItem.Order = existingOrder;

            await orderItemRepository.AddAsync(orderItem);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderItemReadDto>(orderItem);
        }

        public async Task<OrderItemReadDto> DeleteOrderItemByIdAsync(Guid id)
        {
            var orderItemRepository = _unitOfWork.GetRepository<OrderItem>();
            var orderItem = await orderItemRepository.GetByIdAsync(id);
            if (orderItem == null)
                throw new NotFoundException($"Order item not found with id: {id}");

            orderItemRepository.Delete(orderItem);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderItemReadDto>(orderItem);
        }

        public async Task<IEnumerable<OrderItemReadDto>> GetAllOrderItemsAsync()
        {
            var orderItemRepository = _unitOfWork.GetRepository<OrderItem>();
            var orderItems = await orderItemRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderItemReadDto>>(orderItems);
        }

        public async Task<OrderItemReadDto> GetOrderItemByIdAsync(Guid id)
        {
            var orderItemRepository = _unitOfWork.GetRepository<OrderItem>();
            var orderItem = await orderItemRepository.GetByIdAsync(id);
            if (orderItem == null)
                throw new KeyNotFoundException($"Order item not found with id: {id}");

            return _mapper.Map<OrderItemReadDto>(orderItem);
        }

        public async Task<OrderItemReadDto> UpdateOrderItemAsync(OrderItemUpdateDto orderItemUpdateDto)
        {
            if (orderItemUpdateDto == null)
                throw new ArgumentNullException(nameof(orderItemUpdateDto));

            var orderItemRepository = _unitOfWork.GetRepository<OrderItem>();
            var existingOrderItem = await orderItemRepository.GetByIdAsync(orderItemUpdateDto.Id);
            if (existingOrderItem == null)
                throw new KeyNotFoundException($"Order item not found with id: {orderItemUpdateDto.Id}");

            var updatedOrderItem = _mapper.Map(orderItemUpdateDto, existingOrderItem);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderItemReadDto>(updatedOrderItem);
        }
    }
}