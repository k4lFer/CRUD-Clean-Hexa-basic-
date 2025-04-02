using Application.Interfaces.Services;
using Infrastructure.Communication;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Services
{
    class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendNotificationCustomerAsync(string fullName, string email)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", fullName, email);
        }

        public async Task SendNotificationOrderCompletedAsync(Guid orderId)
        {
            await _hubContext.Clients.All.SendAsync("OrderCompleted", orderId);
        }
        public async Task SendNotificationOrderEntranceAsync(List<string> groups,Guid orderId)
        {
            await _hubContext.Clients.Groups(groups).SendAsync("OrderEntrance", orderId);
        }
    }
}
