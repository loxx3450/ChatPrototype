using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolLibrary.Core
{
    public class ProtocolMessageBuilder
    {
        public NetworkStream NetStream { get; set; }

        public ProtocolMessageBuilder(NetworkStream netStream)
        {
            NetStream = netStream;
        }

        public ProtocolMessage GetMessage()
        {
            MemoryStream memStream = GetMessageStream();

            //Filling protocolMessage with data based on stream
            ProtocolMessage protocolMessage = StreamExtractor.ExtractAll(memStream);

            memStream.Close();

            return protocolMessage;
        }

        private MemoryStream GetMessageStream()
        {
            //Finding out the size of data that belongs to message
            int bytesToRead = ConvertToInt(ReadBytes(4));

            MemoryStream memStream = new MemoryStream(bytesToRead);

            memStream.Write(ReadBytes(bytesToRead), 0, bytesToRead);
            memStream.Position = 0;

            return memStream;
        }

        private byte[] ReadBytes(int length)
        {
            byte[] bytes = new byte[length];

            NetStream.ReadExactly(bytes, 0, length);

            return bytes;
        }

        private static int ConvertToInt(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
