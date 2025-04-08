using Domain.Common;


namespace Application.Common.Interfaces
{
    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// Dispatches all domain events from the entity
        /// </summary>
        /// <param name="entity">Entity with domain events</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task representing the async operation</returns>
        Task DispatchAndClearEventsAsync(BaseEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Dispatches domain events without clearing them
        /// </summary>
        Task DispatchEventsAsync(BaseEntity entity, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Checks if the entity has any domain events
        /// </summary>
        bool HasDomainEvents(BaseEntity entity);
    }
}