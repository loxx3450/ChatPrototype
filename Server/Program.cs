Server.Server server = new Server.Server();

server.Start();

while(true)
{
    if (Console.ReadKey().Key == ConsoleKey.Escape)
    {
        server.SocketEvent.StopAcceptingClients();

        break;
    }
}