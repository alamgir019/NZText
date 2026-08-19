using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NZ.Shared.Events;

/// <summary>
/// In-process event bus implementation for the modular monolith.
/// Resolves handlers from DI and dispatches events synchronously within the process.
/// </summary>
public class InProcessEventBus : IEventBus
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<InProcessEventBus> _logger;

    public InProcessEventBus(IServiceProvider serviceProvider, ILogger<InProcessEventBus> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task PublishAsync<TEvent>(TEvent domainEvent, CancellationToken cancellationToken = default)
        where TEvent : IDomainEvent
    {
        _logger.LogDebug("Publishing domain event {EventType} ({EventId})", domainEvent.EventType, domainEvent.EventId);

        var handlers = _serviceProvider.GetServices<IDomainEventHandler<TEvent>>();
        foreach (var handler in handlers)
        {
            try
            {
                await handler.HandleAsync(domainEvent, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error handling domain event {EventType} in {HandlerType}",
                    domainEvent.EventType, handler.GetType().Name);
            }
        }
    }

    public async Task PublishIntegrationAsync<TEvent>(TEvent integrationEvent, CancellationToken cancellationToken = default)
        where TEvent : IIntegrationEvent
    {
        _logger.LogInformation(
            "Publishing integration event {EventType} from module {SourceModule} ({EventId})",
            integrationEvent.EventType, integrationEvent.SourceModule, integrationEvent.EventId);

        var handlers = _serviceProvider.GetServices<IIntegrationEventHandler<TEvent>>();
        foreach (var handler in handlers)
        {
            try
            {
                await handler.HandleAsync(integrationEvent, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error handling integration event {EventType} in {HandlerType}",
                    integrationEvent.EventType, handler.GetType().Name);
            }
        }
    }
}
