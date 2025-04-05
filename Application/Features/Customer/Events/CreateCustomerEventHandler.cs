using Application.Interfaces.Services;
using Domain.Events.TCustomer;
using MediatR;

namespace Application.Features.Customer.Events
{
    public class CreateCustomerEventHandler : INotificationHandler<CreateCustomerEvent>
    {
        private readonly INotificationService _notificationService;

        public CreateCustomerEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(CreateCustomerEvent notification, CancellationToken cancellationToken)
        { 
            await _notificationService.SendNotificationCustomerAsync(notification.fullName, notification.email);
        }
    }
}
