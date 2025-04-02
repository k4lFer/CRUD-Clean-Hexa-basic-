using Application.Interfaces.Services;
using Domain.Events.TOrder;
using MediatR;

namespace Application.Features.Order.Events
{
    public class OrderCompletedEventHandler : INotificationHandler<OrderCompletedEvent>
    {
        private readonly INotificationService _notificationService;
        public OrderCompletedEventHandler(INotificationService notificationService) 
        => _notificationService = notificationService;
        public async Task Handle(OrderCompletedEvent notification, CancellationToken cancellationToken)
        {
            await _notificationService.SendNotificationOrderCompletedAsync(notification.OrderId);
        }
    }
}