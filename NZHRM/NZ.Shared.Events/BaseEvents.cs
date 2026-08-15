namespace NZ.Shared.Events;

public abstract class DomainEvent : IDomainEvent
{
    protected DomainEvent()
    {
        EventId = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
    }

    public Guid EventId { get; }
    public DateTime OccurredOn { get; }
    public abstract string EventType { get; }
}

public abstract class IntegrationEvent : IIntegrationEvent
{
    protected IntegrationEvent(string sourceModule)
    {
        EventId = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
        SourceModule = sourceModule;
    }

    public Guid EventId { get; }
    public DateTime OccurredOn { get; }
    public string SourceModule { get; }
    public abstract string EventType { get; }
}
