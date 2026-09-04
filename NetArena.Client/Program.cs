using System.Net.Sockets;
using NetArena.Shared.Messages;
using NetArena.Shared.Network;

const string Host = "127.0.0.1";
const int Port = 7777;

Console.WriteLine("================================");
Console.WriteLine("         NetArena Client");
Console.WriteLine("================================");
Console.WriteLine($"Connecting to {Host}:{Port}...");

using TcpClient client = new();

await client.ConnectAsync(Host, Port);

Console.WriteLine("Connected to server!");

using NetworkStream stream = client.GetStream();

NetworkMessage? message = await NetworkMessageFraming.ReceiveAsync(stream);

if (message is not null)
{
    Console.WriteLine($"Received message: {message.Type}");
    Console.WriteLine($"Data: {message.Data}");
}
else
{
    Console.WriteLine("Server closed the connection without sending a message.");
}