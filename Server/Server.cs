using SocketEventLibrary.Sockets;
using ChatCore.SocketEventMessages;
using ChatCore.Services;
using ProtocolLibrary.Message;

namespace Server
{
    public class Server
    {
        public ServerSocketEvent SocketEvent { get; set; }

        public List<Client> Clients { get; set; }

        public Server(string host = "127.0.0.1", int port = 8080)
        {
            SocketEvent = new ServerSocketEvent(host, port);

            Clients = new List<Client>();
        }

        public void Start()
        {
            SocketEvent.OnClientIsConnected += SetupSocket;

            SocketEvent.StartAcceptingClients();
        }

        private void SetupSocket(SocketEvent socket)
        {
            //1. Setting supported SocketMessage's Types for income
            socket.AddSupportedMessageType<SocketEventProtocolMessage>();

            //2. Setting callbacks
            socket.On(ProtocolMessageType.AuthRequest, mes =>
            {
                ProtocolMessage message = (ProtocolMessage)mes;

                AuthorizationService authService = new AuthorizationService();

                authService.Handle(message);
            });

            //3. Setting callbacks to events
            //....

            //Adding SocketEvent to the collection of Clients(Network Streams) that are representing server side
            Clients.Add(new Client(socket));
        }
    }
}
