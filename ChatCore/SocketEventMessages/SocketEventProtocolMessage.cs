using ProtocolLibrary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketEventLibrary.SocketEventMessageCore;
using ProtocolLibrary.Message;

namespace ChatCore.SocketEventMessages
{
    public class SocketEventProtocolMessage : SocketEventMessage, IRecoverable
    {
        public SocketEventProtocolMessage(string key, ProtocolMessage argument) 
            : base(key, argument)
        { }

        public override MemoryStream GetDataStream()
        {
            MemoryStream memoryStream = new MemoryStream(); 

            //Writing Key
            using StreamWriter writer = new StreamWriter(memoryStream, leaveOpen:true);
            writer.WriteLine(Convert.ToString(Key));
            writer.Flush();

            //Writing ProtocolMessage as an argument
            ProtocolMessage message = (ProtocolMessage)Payload;
            message.GetStream().CopyTo(memoryStream);

            memoryStream.Position = 0;

            return memoryStream;
        }

        public static SocketEventMessage RecoverSocketEventMessage(MemoryStream memoryStream)
        {
            int pos = (int)memoryStream.Position;

            //Reading Key
            using StreamReader reader = new StreamReader(memoryStream, leaveOpen: true);
            string key = reader.ReadLine() ?? throw new Exception();

            //TEMP
            memoryStream.Position = pos + key.Length + 2;

            //Getting ProtocolMessage from Stream
            ProtocolMessage argument = StreamExtractor.ExtractAll(memoryStream);

            return new SocketEventProtocolMessage(key, argument);
        }
    }
}
