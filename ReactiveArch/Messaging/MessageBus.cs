using ReactiveArch.Models;

namespace ReactiveArch.Messaging;
using System.Threading.Channels;

public class MessageBus
{
    private readonly Channel<EventMessage> _channel;

    public MessageBus(int capacity)
    {
        var options = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        _channel = Channel.CreateBounded<EventMessage>(options);
    }

    public async ValueTask PublishAsync(EventMessage message)
    {
        await _channel.Writer.WriteAsync(message);
    }

    public IAsyncEnumerable<EventMessage> SubscribeAsync(CancellationToken ct)
        => _channel.Reader.ReadAllAsync(ct);

    public void Complete() => _channel.Writer.Complete();

    public int EstimatedQueueSize => _channel.Reader.Count;
}
