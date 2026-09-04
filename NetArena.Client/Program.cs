using System.Net.Sockets;

const string Host = "127.0.0.1";
const int Port = 7777;

Console.WriteLine("================================");
Console.WriteLine("         NetArena Client");
Console.WriteLine("================================");
Console.WriteLine($"Connecting to {Host}:{Port}...");

using TcpClient client = new();

await client.ConnectAsync(Host, Port);

Console.WriteLine("Connected to server!");