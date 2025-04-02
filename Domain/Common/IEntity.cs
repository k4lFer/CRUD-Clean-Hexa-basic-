namespace Domain.Common
{
    public interface IEntity
    {
        Guid id { get; }
        IReadOnlyCollection<BaseEvent> DomainEvents { get; }
    }

}