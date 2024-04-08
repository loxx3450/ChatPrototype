using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SocketEventLibrary;
using ProtocolLibrary;
using ProtocolLibrary.Core;
using ProtocolLibrary.Payload;
using SocketEventLibrary.Sockets;

namespace Server
{
    public class Client
    {
        public SocketEvent SocketEvent { get; set; }

        public Client(SocketEvent socketEvent)
        {
            SocketEvent = socketEvent;
        }
    }
}
