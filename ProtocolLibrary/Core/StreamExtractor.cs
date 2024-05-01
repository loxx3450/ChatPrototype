using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using ProtocolLibrary.Message;

namespace ProtocolLibrary.Core
{
    //The only task is to get ProtocolMessage from MemoryStream
    public static class StreamExtractor
    {
        public static ProtocolMessage ExtractAll(MemoryStream memStream)
        {
            ProtocolMessage protocolMessage = new ProtocolMessage();

            using (StreamReader reader = new StreamReader(memStream, leaveOpen:true))
            {
                ExtractHeaders(protocolMessage, reader);

                ExtractPayload(protocolMessage, memStream);
            }

            return protocolMessage;
        }


        //Extracting Headers
        private static void ExtractHeaders(ProtocolMessage message, StreamReader reader)
        {
            string? header;

            while (true)
            {
                header = reader.ReadLine();

                if (string.IsNullOrEmpty(header))
                    break;

                message.SetHeader(header);
            }
        }


        //Extracting Payload
        //Info about Payload will be saved as a Stream
        //and should be converted then
        private static void ExtractPayload(ProtocolMessage message, MemoryStream memStream)
        {
            int payloadLength = message.PayloadLength;

            //Position right before Payload
            memStream.Position = memStream.Length - payloadLength;

            MemoryStream payloadStream = new MemoryStream(payloadLength);
            memStream.CopyTo(payloadStream);
            payloadStream.Position = 0;

            message.PayloadStream = payloadStream;
        }
    }
}
