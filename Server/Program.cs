Server.Server server = new Server.Server();

server.Start();

while(true)
{
    if (Console.ReadLine() == "stop")
        server.SocketEvent.StopAcceptingClients();
}