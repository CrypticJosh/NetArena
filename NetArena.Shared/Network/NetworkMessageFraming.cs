using System.Buffers.Binary;
using System.Net.Sockets;
using NetArena.Shared.Messages;

namespace NetArena.Shared.Network;

public static class NetworkMessageFraming
{
    public static async Task SendAsync(
        NetworkStream stream,
        NetworkMessage message,
        CancellationToken cancellationToken = default)
    {
        byte[] payload = NetworkMessageSerializer.Serialize(message);

        byte[] header = new byte[4];

        BinaryPrimitives.WriteInt32BigEndian(
            header,
            payload.Length);

        await stream.WriteAsync(
            header,
            cancellationToken);

        await stream.WriteAsync(
            payload,
            cancellationToken);
    }

    public static async Task<NetworkMessage?> ReceiveAsync(
        NetworkStream stream,
        CancellationToken cancellationToken = default)
    {
        byte[] header = new byte[4];

        int bytesRead = await ReadExactlyAsync(
            stream,
            header,
            cancellationToken);

        if (bytesRead == 0)
        {
            return null;
        }

        int payloadLength = BinaryPrimitives.ReadInt32BigEndian(header);

        if (payloadLength <= 0)
        {
            throw new InvalidDataException(
                $"Invalid message length: {payloadLength}");
        }

        byte[] payload = new byte[payloadLength];

        await ReadExactlyAsync(
            stream,
            payload,
            cancellationToken);

        return NetworkMessageSerializer.Deserialize(payload);
    }

    private static async Task<int> ReadExactlyAsync(
        NetworkStream stream,
        byte[] buffer,
        CancellationToken cancellationToken)
    {
        int totalRead = 0;

        while (totalRead < buffer.Length)
        {
            int bytesRead = await stream.ReadAsync(
                buffer.AsMemory(totalRead),
                cancellationToken);

            if (bytesRead == 0)
            {
                return totalRead;
            }

            totalRead += bytesRead;
        }

        return totalRead;
    }
}
