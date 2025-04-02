using Application.DTOs.Order;
using Domain.Entities;
using Shared.Message;

namespace Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Message> CreateOrderAsync(OrderCreateDto order, CancellationToken cancellationToken = default);
        Task<bool> ProcessOrderAsync(Guid orderId);
        Task<Message> CancelOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
    }
}