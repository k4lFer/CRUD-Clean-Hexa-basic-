using Application.Common.Interfaces;
using Application.DTOs.Order;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Features.Order.Services
{
    public partial class OrderService
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
        #region Private Methods

        private async Task<TOrder> GetValidOrderAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            return order == null || order.IsCancelled() ? null : order;
        }

        private async Task SaveOrderChangesAsync(TOrder order)
        {
            await _orderRepository.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync(default);
        }

        private async Task<TOrder> CreateAndSaveOrderAsync(
            OrderCreateDto orderDto, 
            ICollection<TProduct> products,
            CancellationToken cancellationToken)
        {
            var order = BuildOrder(orderDto, products);
            await SaveOrderWithDetailsAsync(order, cancellationToken);
            return order;
        }

        private TOrder BuildOrder(OrderCreateDto orderDto, ICollection<TProduct> products)
        {
            var items = orderDto.orderDetails
                .Select(d => (
                    d.productId,
                    d.quantity,
                    products.First(p => p.id == d.productId).price
                ))
                .ToList(); 
            return TOrder.Create(orderDto.customerId, items);
        }

        private async Task SaveOrderWithDetailsAsync(TOrder order, CancellationToken cancellationToken)
        {
            await _orderRepository.AddAsync(order, cancellationToken);
            await _orderDetailRepository.CreateRangeAsync(order.OrderDetails.ToList(), cancellationToken);
        }

        private async Task UpdateProductsStockAsync(
            ICollection<OrderDetailCreateDto> orderDetails, 
            ICollection<TProduct> products,
            CancellationToken cancellationToken)
        {
            AdjustProductsStock(orderDetails, products);
            await _productRepository.UpdateRankAsync(products.ToList(), cancellationToken);
        }

        private void AdjustProductsStock(ICollection<OrderDetailCreateDto> orderDetails, ICollection<TProduct> products)
        {
            foreach (var detail in orderDetails)
            {
                var product = products.First(p => p.id == detail.productId);
                product.DecreaseStock(detail.quantity);
            }
        }

        private void RestoreProductsStock(TOrder order, ICollection<TProduct> products)
        {
            foreach (var detail in order.OrderDetails)
            {
                var product = products.FirstOrDefault(p => p.id == detail.productId);
                product?.IncreaseStock(detail.quantity);
            }
        }

        private async Task FinalizeOrderCreationAsync(TOrder order, CancellationToken cancellationToken)
        {
            order.CompleteOrder();
            await _orderRepository.UpdateAsync(order, cancellationToken);
            await DispatchOrderEventsAsync(order, cancellationToken);
        }

        private async Task DispatchOrderEventsAsync(TOrder order, CancellationToken cancellationToken)
        {
            await _dispatcher.DispatchAndClearEventsAsync(order, cancellationToken);
        }

        #endregion
    }
}