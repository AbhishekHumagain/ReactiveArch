using ReactiveArch.Messaging;
using ReactiveArch.Models;
using ReactiveArch.Utils;

namespace ReactiveArch.Services;

public class ConsumerService(MessageBus bus, BackpressureController controller) : IEventConsumer
{
    public async Task StartAsync()
    {
        Console.WriteLine("Consumer listening (reactive)...");

        var cts = new CancellationTokenSource();

        await foreach (var message in bus.SubscribeAsync(cts.Token))
        {
            await controller.ApplyBackpressureIfNeededAsync();

            await OnEventReceivedAsync(message);
        }
    }

    public async Task OnEventReceivedAsync(EventMessage message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Consumed -> {message}");
        Console.ResetColor();

        // Simulate slow consumer workload
        await Task.Delay(300);
    }
}