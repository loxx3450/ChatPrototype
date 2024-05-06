using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using ProtocolLibrary.Message;

namespace ProtocolLibrary.Core
{
    /// <summary>
    /// Static class <c>ProtocolMessageStreamBuilder</c> provides logic of 
    /// transforming MemoryStream into ProtocolMessage.
    /// </summary>
    public static class StreamExtractor
    {
        /// <summary>
        /// This method creates ProtocolMessage basing on <paramref name="memStream"/>.
        /// The result will contain only PayloadStream, not already interpetated IPayload object.
        /// </summary>
        /// <returns>The ProtocolMessage that was built by <paramref name="memStream"/>.</returns>
        public static ProtocolMessage ExtractAll(MemoryStream memStream)
        {
            ProtocolMessage protocolMessage = new ProtocolMessage();

            using StreamReader reader = new StreamReader(memStream, leaveOpen: true);

            ExtractHeaders(protocolMessage, reader);

            //Simply copies the rest of memStream to PayloadStream
            ExtractPayload(protocolMessage, memStream);

            return protocolMessage;
        }


        //Extracts Headers
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


        //Extracts Payload
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
