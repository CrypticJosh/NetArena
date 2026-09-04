namespace NetArena.Shared.Messages;

public class NetworkMessage
{
    public MessageType Type { get; set; }

    public string Data { get; set; } = string.Empty;
}