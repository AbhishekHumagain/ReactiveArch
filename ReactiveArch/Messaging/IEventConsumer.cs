using ReactiveArch.Models;

namespace ReactiveArch.Messaging;

public interface IEventConsumer
{
    Task OnEventReceivedAsync(EventMessage message);
}