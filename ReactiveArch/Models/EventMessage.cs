namespace ReactiveArch.Models;

public class EventMessage
{
    public int Id { get; init; }
    public DateTime Timestamp { get; init; }
    public string Payload { get; init; } = "";

    public override string ToString()
    {
        return $"[Event {Id}] {Payload} at {Timestamp:HH:mm:ss.fff}";
    }
}