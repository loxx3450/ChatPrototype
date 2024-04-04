using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EventSocket;
using ProtocolLibrary;
using ProtocolLibrary.Core;
using ProtocolLibrary.PayloadTypes;
using EventSocket.Sockets;

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
