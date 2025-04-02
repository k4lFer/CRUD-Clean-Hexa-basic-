using Application.Common.Exceptions;
using Application.DTOs.Order;
using Domain.Entities;

namespace Application.Features.Order.Services
{
    public partial class OrderService
    {
         #region Private Methods

        private async Task<TOrder> GetValidOrderAsync(Guid orderId)
        {
            return await _orderRepository.GetByIdAsync(orderId) 
                ?? throw new BaseException("Orden no encontrada");
        }

        private async Task SaveOrderChangesAsync(TOrder order)
        {
            await _orderRepository.UpdateAsync(order);
            await _unitOfWork.CommitAsync();
        }

        private async Task<ICollection<TProduct>> ValidateProductsAndStockAsync(
            ICollection<OrderDetailCreateDto> orderDetails, 
            CancellationToken cancellationToken)
        {
            var productIds = orderDetails.Select(d => d.productId).ToList();
            var products = await _productRepository.GetByIdsAsync(productIds, cancellationToken);

            foreach (var detail in orderDetails)
            {
                ValidateProductStock(detail, products);
            }

            return products;
        }

        private void ValidateProductStock(OrderDetailCreateDto detail, ICollection<TProduct> products)
        {
            var product = products.FirstOrDefault(p => p.id == detail.productId) 
                ?? throw new BaseException($"Producto ID {detail.productId} no encontrado");
                
            if (product.stock < detail.quantity)
                throw new BaseException($"Stock insuficiente para {product.name}");
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
           /* return TOrder.Create(
                orderDto.customerId,
                orderDto.orderDetails.Select(d => (
                    d.productId,
                    d.quantity,
                    products.First(p => p.id == d.productId).price)
                ));
                */
            var items = orderDto.orderDetails
                .Select(d => (
                    d.productId,
                    d.quantity,
                    products.First(p => p.id == d.productId).price
                ))
                .ToList();  // Materializar aquí
            
            return TOrder.Create(orderDto.customerId, items);
        }

        private async Task SaveOrderWithDetailsAsync(TOrder order, CancellationToken cancellationToken)
        {
            await _orderRepository.AddAsync(order, cancellationToken);
            
            // Materializar la lista antes de pasarla al repositorio
            var orderDetails = order.OrderDetails.ToList();
            await _orderDetailRepository.CreateRangeAsync(orderDetails, cancellationToken);
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
                products.First(p => p.id == detail.productId).DecreaseStock(detail.quantity);
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

        private async Task HandleOrderCreationErrorAsync(CancellationToken cancellationToken)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
        }

        #endregion
    }
}