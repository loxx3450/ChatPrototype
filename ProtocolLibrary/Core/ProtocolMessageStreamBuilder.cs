using ProtocolLibrary.Payload;
using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolLibrary.Core
{
    //The only task is to get MemoryStream from ProtocolMessage
    public static class ProtocolMessageStreamBuilder
    {
        public static MemoryStream GetStream(ProtocolMessage message)
        {
            MemoryStream memStream = new MemoryStream();

            //Writes headers
            using StreamWriter writer = new StreamWriter(memStream, leaveOpen: true);
            WriteHeaders(message, writer);

            //The rest is Stream(optional)
            WritePayloadIfExists(message, memStream);

            //Stream should be able for work
            memStream.Position = 0;

            return memStream;
        }


        //Writing all headers of Message
        private static void WriteHeaders(ProtocolMessage message, StreamWriter writer)
        {
            foreach (var header in message.Headers)
                writer.WriteLine($"{header.Key}:{header.Value}");

            //Empty line between headers and payload
            writer.WriteLine();

            writer.Flush();
        }


        //PayloadStream is possibly null
        private static void WritePayloadIfExists(ProtocolMessage message, MemoryStream stream)
        {
            if (message.PayloadStream is null)
                return;

            message.PayloadStream.Position = 0;
            message.PayloadStream.CopyTo(stream);

            message.PayloadStream.Position = 0;
        }
    }
}
