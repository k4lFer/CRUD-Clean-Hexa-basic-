using Application.Interfaces;
using Domain.Common;
using MediatR;

namespace Infrastructure.Services
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchAndClearEventsAsync(BaseEntity entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!HasDomainEvents(entity)) return;
                
                var events = entity.DomainEvents.ToList(); // Snapshot
                await Task.WhenAll(events.Select(e => _mediator.Publish(e, cancellationToken)));
            }
            finally
            {
                entity.ClearDomainEvents(); // Garantiza limpieza incluso con errores
            }
        }

        public async Task DispatchEventsAsync(BaseEntity entity, CancellationToken cancellationToken = default)
        {
            if (!HasDomainEvents(entity)) return;

            var tasks = entity.DomainEvents
                .Select(domainEvent => _mediator.Publish(domainEvent, cancellationToken))
                .ToList();

            await Task.WhenAll(tasks);
        }

        public bool HasDomainEvents(BaseEntity entity)
        {
            return entity.DomainEvents?.Any() == true;
        }
    }
}