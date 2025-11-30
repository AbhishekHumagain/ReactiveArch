using ReactiveArch.Messaging;

namespace ReactiveArch.Utils;

public class BackpressureController(MessageBus bus)
{
    private const int HighWatermark = 4; // threshold when backpressure applies

    public async Task ApplyBackpressureIfNeededAsync()
    {
        int queueSize = bus.EstimatedQueueSize;

        if (queueSize >= HighWatermark)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[Backpressure] Queue high ({queueSize}). Slowing consumer...");
            Console.ResetColor();

            await Task.Delay(300); // artificial slow-down
        }
    }
}