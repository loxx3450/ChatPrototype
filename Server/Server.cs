using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        public string Host { get; set; }
        public int Port { get; set; }   

        private TcpListener listener = null!;

        public List<Client> Clients { get; set; }

        public Server(string host = "127.0.0.1", int port = 8080)
        {
            Host = host;
            Port = port;

            listener = new TcpListener(IPAddress.Parse(host), port);

            Clients = new List<Client>();
        }

        public async Task RunAsync()
        {
            try
            {
                listener.Start();
                Console.WriteLine($"Server started at {Host}:{Port}");

                while (true) 
                { 
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();

                    Client client = new Client(tcpClient);

                    Clients.Add(client);

                    _ = Task.Run(() => client.RunProcessing());
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        ~Server()
        {
            listener.Stop();
        }
    }
}
