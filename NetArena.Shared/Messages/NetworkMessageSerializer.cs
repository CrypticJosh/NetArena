using System.Text.Json;

namespace NetArena.Shared.Messages;

public static class NetworkMessageSerializer
{
    public static byte[] Serialize(NetworkMessage message)
    {
        return JsonSerializer.SerializeToUtf8Bytes(message);
    }

    public static NetworkMessage? Deserialize(byte[] data)
    {
        return JsonSerializer.Deserialize<NetworkMessage>(data);
    }
}