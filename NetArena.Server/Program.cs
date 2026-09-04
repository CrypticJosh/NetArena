using System.Net;
using System.Net.Sockets;

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

    client.Close();
}