using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ProtocolLibrary;
using ProtocolLibrary.Core;
using ProtocolLibrary.PayloadTypes;

namespace Server
{
    public class Client
    {
        public TcpClient TcpClient { get; set; }
        public NetworkStream NetStream { get; set; }

        public Client(TcpClient tcpClient)
        {
            TcpClient = tcpClient;
        }

        public void RunProcessing()
        {
            try
            {
                NetStream = TcpClient.GetStream();

                ProtocolMessageBuilder builder = new ProtocolMessageBuilder(NetStream);
                
                while (true)
                {
                    ProtocolMessage message = builder.GetMessage();

                    Console.WriteLine(message.MessageType.ToString());

                    Console.WriteLine("Headers:");
                    foreach(var item in message.Headers) 
                    { 
                        Console.WriteLine(item.Key + ':' + item.Value);
                    }
                    Console.WriteLine();

                    using (StreamReader reader = new StreamReader(message.PayloadStream))
                    {
                        string json = reader.ReadToEnd(); // Read JSON from the stream
                        AuthRequestPayload? p = JsonSerializer.Deserialize<AuthRequestPayload>(json);

                        Console.WriteLine(p.Login);
                        Console.WriteLine(p.Password);
                    }

                    //Handle

                    //Send Response
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}
