using Microsoft.Extensions.DependencyInjection;

namespace NZ.Shared.Events;

public static class EventBusRegistration
{
    /// <summary>
    /// Registers the in-process event bus. Call this in Program.cs or Shared module setup.
    /// </summary>
    public static IServiceCollection AddEventBus(this IServiceCollection services)
    {
        services.AddSingleton<IEventBus, InProcessEventBus>();
        return services;
    }
}
