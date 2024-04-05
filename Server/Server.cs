using EventSocket.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ProtocolLibrary.SocketEventMessages;
using ProtocolLibrary.PayloadTypes;
using ProtocolLibrary;
using System.Text.Json;
using ProtocolLibrary.Services;

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
            socket.On("AuthRequest", mes =>
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
