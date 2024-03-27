using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}
