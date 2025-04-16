using Application.DTOs.Common;
using Application.DTOs.Order;
using Domain.Entities;
using Shared.Message;

namespace Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Result<object>> CreateOrderAsync(OrderCreateDto order, CancellationToken cancellationToken = default);
        Task<bool> ProcessOrderAsync(Guid orderId);
        Task<Result<object>> CancelOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
    }
}