using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolLibrary.Core
{
    internal static class StreamExtractor
    {
        public static ProtocolMessage ExtractAll(MemoryStream memStream)
        {
            ProtocolMessage protocolMessage = new ProtocolMessage();

            using (StreamReader reader = new StreamReader(memStream))
            {
                ExtractMessageType(protocolMessage, reader);

                ExtractHeaders(protocolMessage, reader);

                ExtractPayload(protocolMessage, memStream);
            }

            return protocolMessage;
        }

        private static void ExtractMessageType(ProtocolMessage message, StreamReader reader)
        {
            string? messageType = reader.ReadLine();

            switch (messageType)
            {
                case "AuthRequest":
                    message.MessageType = ProtocolMessageType.AuthRequest;
                    break;
                case "AuthResponse":
                    message.MessageType = ProtocolMessageType.AuthResponse;
                    break;
                default:
                    message.MessageType = ProtocolMessageType.None;
                    break;
            }
        }

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

        private static void ExtractPayload(ProtocolMessage message, MemoryStream memStream)
        {
            int payloadLength = message.PayloadLength;

            memStream.Position = memStream.Length - payloadLength;

            MemoryStream payloadStream = new MemoryStream(payloadLength);
            memStream.CopyTo(payloadStream);
            payloadStream.Position = 0;

            message.PayloadStream = payloadStream;
        }
    }
}
