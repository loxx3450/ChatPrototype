using ProtocolLibrary.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolLibrary.Core
{
    public static class ProtocolMessageStreamBuilder
    {
        public static MemoryStream GetStream(ProtocolMessage message)
        {
            //Inserting first 4 bytes for the future message size
            MemoryStream memStream = new MemoryStream();
            memStream.Write(new byte[4], 0, 4);

            //Writing all parts of message
            using (StreamWriter writer = new StreamWriter(memStream, leaveOpen:true))
                WriteMainInfo(message, writer);

            WritePayloadIfExists(message, memStream);

            memStream.Position = 0;

            //Inserting message size
            int messageSize = (int)memStream.Length - 4;

            memStream.Write(ConvertIntToBytes(messageSize), 0, 4);

            //Returning finished stream
            memStream.Position = 0;

            return memStream;
        }

        private static void WriteMainInfo(ProtocolMessage message, StreamWriter writer)
        {
            writer.WriteLine(message.MessageType.ToString());

            foreach (var header in message.Headers)
                writer.WriteLine($"{header.Key}:{header.Value}");

            //Empty line between headers and payload
            writer.WriteLine();

            writer.Flush();
        }

        private static void WritePayloadIfExists(ProtocolMessage message, MemoryStream stream)
        {
            if (message.PayloadStream is null)
                return;

            message.PayloadStream.Position = 0;
            message.PayloadStream.CopyTo(stream);

            message.PayloadStream.Position = 0;
        }

        private static byte[] ConvertIntToBytes(int val)
        {
            byte[] bytes = BitConverter.GetBytes(val);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return bytes;
        }
    }
}
