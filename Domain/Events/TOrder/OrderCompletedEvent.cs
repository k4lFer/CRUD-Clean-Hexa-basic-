using Domain.Common;

namespace Domain.Events.TOrder
{
    public class OrderCompletedEvent : BaseEvent
    {
        // Implementacion mas adelante para mandar notificaciones con SignalR
        public Guid OrderId { get; }

        public OrderCompletedEvent(Guid orderId)
            => OrderId = orderId;
    
    }
}