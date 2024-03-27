using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ProtocolLibrary;
using ProtocolLibrary.Core;

namespace Server
{
    public class Client
    {
        public TcpClient TcpClient { get; set; }

        public Client(TcpClient tcpClient)
        {
            TcpClient = tcpClient;
        }

        public void RunProcessing()
        {
            try
            {
                ProtocolMessageBuilder builder = new ProtocolMessageBuilder(TcpClient.GetStream());
                
                while (true)
                {
                    ProtocolMessage message = builder.GetMessage();

                    foreach(var item in message.Headers) 
                    { 
                        Console.WriteLine(item.Key + ':' + item.Value);
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
