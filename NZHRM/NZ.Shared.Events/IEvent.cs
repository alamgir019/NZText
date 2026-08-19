namespace NZ.Shared.Events;

/// <summary>
/// Marker interface for all domain events.
/// Domain events communicate state changes within a module boundary.
/// </summary>
public interface IDomainEvent
{
    Guid EventId { get; }
    DateTime OccurredOn { get; }
    string EventType { get; }
}

/// <summary>
/// Marker interface for integration events.
/// Integration events cross module boundaries and trigger reactions in other modules.
/// </summary>
public interface IIntegrationEvent
{
    Guid EventId { get; }
    DateTime OccurredOn { get; }
    string EventType { get; }
    string SourceModule { get; }
}
