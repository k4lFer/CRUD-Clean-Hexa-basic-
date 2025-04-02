using Application.Common.Exceptions;
using Application.DTOs.Order;
using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Shared.Message;

namespace Application.Features.Order.Services
{
    public partial class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IDomainEventDispatcher _dispatcher;

        public OrderService(
            IOrderRepository orderRepository,
            IUnitOfWork unitOfWork,
            IProductRepository productRepository,
            IOrderDetailRepository orderDetailRepository,
            IDomainEventDispatcher dispatcher)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _orderDetailRepository = orderDetailRepository;
            _dispatcher = dispatcher;
        }

        public async Task<Message> CancelOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            var message = new Message();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var order = await _orderRepository.GetByIdWithDetailsAsync(orderId, cancellationToken) 
                    ?? throw new BaseException("Orden no encontrada");
                var productIds = order.OrderDetails.Select(od => od.productId).ToList();
                var products = await _productRepository.GetByIdsAsync(productIds, cancellationToken);

                foreach (var detail in order.OrderDetails)
                {
                    var product = products.FirstOrDefault(p => p.id == detail.productId);
                    product?.IncreaseStock(detail.quantity);
                }

                await _productRepository.UpdateRankAsync(products, cancellationToken);

                order.CancelOrder();
                await _orderRepository.UpdateAsync(order, cancellationToken);

                await _unitOfWork.CommitAsync(cancellationToken);

                message.Success();
                message.AddMessage("Orden cancelada y stock restaurado exitosamente");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                message.Error();
                message.AddMessage($"Error al cancelar orden: {ex.Message}");
            }

            return message;
        }

        public async Task<Message> CreateOrderAsync(OrderCreateDto orderDto, CancellationToken cancellationToken)
        {
            var message = new Message();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var products = await ValidateProductsAndStockAsync(orderDto.orderDetails, cancellationToken);
                var order = await CreateAndSaveOrderAsync(orderDto, products, cancellationToken);
                await UpdateProductsStockAsync(orderDto.orderDetails, products, cancellationToken);
               // await FinalizeOrderCreationAsync(order, cancellationToken);

                await _unitOfWork.CommitAsync(cancellationToken);
                message.Success();
                message.AddMessage("Orden creada con éxito");
            }
            catch (Exception ex)
            {
                await HandleOrderCreationErrorAsync(cancellationToken);
                message.Error();
                message.AddMessage($"Error al crear la orden: {ex.Message}");
            }

            return message;
        }

        public async Task<bool> ProcessOrderAsync(Guid orderId)
        {
            var order = await GetValidOrderAsync(orderId);
            order.ProcessOrder();
            await SaveOrderChangesAsync(order);
            return true;
        }

    }
}