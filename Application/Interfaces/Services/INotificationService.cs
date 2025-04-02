namespace Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task SendNotificationCustomerAsync(string fullName, string email);
        Task SendNotificationOrderCompletedAsync(Guid orderId);
    }
}
