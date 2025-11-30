using ReactiveArch.Messaging;
using ReactiveArch.Services;
using ReactiveArch.Utils;

namespace ReactiveArch;

internal abstract class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Reactive Architecture Demo (.NET 10)\n");

        // Message bus is the backbone of reactive communication.
        var messageBus = new MessageBus(capacity: 5); // small capacity => backpressure demo
        var backpressureController = new BackpressureController(messageBus);

        var producer = new ProducerService(messageBus);
        var consumer = new ConsumerService(messageBus, backpressureController);

        // Start consumer => reactively listens
        var consumerTask = consumer.StartAsync();

        // Produce events
        await producer.StartProducingAsync();

        // Give time for all messages to finish
        await Task.Delay(2000);

        messageBus.Complete();

        await consumerTask;

        Console.WriteLine("\nDemo Completed.");
    }
}