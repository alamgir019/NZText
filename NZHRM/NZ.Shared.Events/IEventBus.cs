namespace NZ.Shared.Events;

/// <summary>
/// Handles a specific domain event type.
/// </summary>
public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);
}

/// <summary>
/// Handles a specific integration event type.
/// </summary>
public interface IIntegrationEventHandler<in TEvent> where TEvent : IIntegrationEvent
{
    Task HandleAsync(TEvent integrationEvent, CancellationToken cancellationToken = default);
}

/// <summary>
/// In-process event bus for publishing events within the modular monolith.
/// </summary>
public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent domainEvent, CancellationToken cancellationToken = default)
        where TEvent : IDomainEvent;

    Task PublishIntegrationAsync<TEvent>(TEvent integrationEvent, CancellationToken cancellationToken = default)
        where TEvent : IIntegrationEvent;
}
