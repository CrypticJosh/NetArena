using System.Net;
using System.Net.Sockets;
using NetArena.Shared.Messages;
using NetArena.Shared.Network;

const int Port = 7777;

TcpListener listener = new(IPAddress.Any, Port);

listener.Start();

Console.WriteLine("================================");
Console.WriteLine("         NetArena Server");
Console.WriteLine("================================");
Console.WriteLine($"Listening on port {Port}...");
Console.WriteLine("Waiting for connections...");

while (true)
{
    TcpClient client = await listener.AcceptTcpClientAsync();

    Console.WriteLine($"Client connected: {client.Client.RemoteEndPoint}");

    using NetworkStream stream = client.GetStream();

    NetworkMessage welcomeMessage = new()
    {
        Type = MessageType.Welcome,
        Data = "Welcome to NetArena!"
    };

    await NetworkMessageFraming.SendAsync(
        stream,
        welcomeMessage);

    Console.WriteLine("Welcome message sent.");

    client.Close();
}