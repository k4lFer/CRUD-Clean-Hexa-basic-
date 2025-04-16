using Application.DTOs.Common;
using Application.DTOs.Order;
using Application.Interfaces.Services;
using Shared.Message;
using System.Data;

namespace Application.Features.Order.Services
{
    public partial class OrderService : IOrderService
    {
              
        public async Task<Result<object>> CancelOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetByIdWithDetailsAsync(orderId, cancellationToken);
            if (order == null)
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                return Result<object>.Error("Orden no encontrada");
            }

            if (order.IsCancelled())
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                return Result<object>.Error("Orden ya cancelada");
            }
            if (order.IsCompleted())
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                return Result<object>.Error("Orden ya completada, no se puede cancelar");
            }

            await _unitOfWork.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
            
            order = await _orderRepository.GetByIdWithDetailsAsync(orderId, cancellationToken);                                        
            var productIds = order.OrderDetails.Select(od => od.productId).ToList();
            var products = await _productRepository.GetByIdsAsync(productIds, cancellationToken);

            RestoreProductsStock(order, products);
            
            await _productRepository.UpdateRankAsync(products, cancellationToken);

            order.CancelOrder();
            await _orderRepository.UpdateAsync(order, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Result<object>.Success("Orden cancelada con éxito");
        }

        public async Task<Result<object>> CreateOrderAsync(OrderCreateDto orderDto, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(IsolationLevel.RepeatableRead, cancellationToken);

            var products = await _productRepository.GetByIdsAsync(orderDto.orderDetails.Select(d => d.productId).ToList(), cancellationToken);
            var order = await CreateAndSaveOrderAsync(orderDto, products, cancellationToken);
            await UpdateProductsStockAsync(orderDto.orderDetails, products, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);
            //await FinalizeOrderCreationAsync(order, cancellationToken);
            
            return Result<object>.Created($"Orden creada con éxito ID: {order.id}");
        }

        public async Task<bool> ProcessOrderAsync(Guid orderId)
        {
            var order = await GetValidOrderAsync(orderId);
            if (order == null) return false;
            
            order.ProcessOrder();
            await SaveOrderChangesAsync(order);
            return true;
        }

    }
}