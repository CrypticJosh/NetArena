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

    _ = HandleClientAsync(client);
}

static async Task HandleClientAsync(TcpClient client)
{
    using (client)
    {
        try
        {
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

            while (true)
            {
                NetworkMessage? message =
                    await NetworkMessageFraming.ReceiveAsync(stream);

                if (message is null)
                {
                    Console.WriteLine("Client disconnected.");
                    break;
                }

                Console.WriteLine(
                    $"Received message: {message.Type}");

                Console.WriteLine(
                    $"Data: {message.Data}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Client connection error: {ex.Message}");
        }
    }
}