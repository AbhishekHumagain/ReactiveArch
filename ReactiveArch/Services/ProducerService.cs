using ReactiveArch.Messaging;
using ReactiveArch.Models;

namespace ReactiveArch.Services;

public class ProducerService(MessageBus messageBus)
{
    public async Task StartProducingAsync()
    {
        Console.WriteLine("Producer started...");

        for (int i = 1; i <= 15; i++)
        {
            var evt = new EventMessage
            {
                Id = i,
                Timestamp = DateTime.Now,
                Payload = $"Event payload {i}"
            };

            Console.WriteLine($"Producing -> {evt}");
            await messageBus.PublishAsync(evt); // async & message-driven

            await Task.Delay(150); // produce faster than consumer => backpressure
        }
    }
}