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
            socket.On("MessageToServer", (arg) =>
            {
                ProtocolMessage message = (ProtocolMessage)arg;

                //TEST CODE
                Console.WriteLine(message.MessageType.ToString());

                Console.WriteLine("Headers:");
                foreach (var item in message.Headers)
                {
                    Console.WriteLine(item.Key + ':' + item.Value);
                }
                Console.WriteLine();

                using (StreamReader reader = new StreamReader(message.PayloadStream, leaveOpen: true))
                {
                    string json = reader.ReadToEnd(); // Read JSON from the stream
                    AuthRequestPayload? p = JsonSerializer.Deserialize<AuthRequestPayload>(json);

                    Console.WriteLine(p.Login);
                    Console.WriteLine(p.Password);
                }
            });

            //3. Setting callbacks to events
            //....

            //Adding SocketEvent to the collection of Clients(Network Streams) that are representing server side
            Clients.Add(new Client(socket));
        }
    }
}
