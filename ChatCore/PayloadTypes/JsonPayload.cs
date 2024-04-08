using ProtocolLibrary.Payload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatCore.PayloadTypes
{
    public abstract class JsonPayload : IPayload
    {
        public string GetPayloadType()
        {
            return "json";
        }

        public MemoryStream GetStream()
        {
            byte[] bytes = Encoding.UTF8.GetBytes(GetJson());

            MemoryStream memStream = new MemoryStream(bytes.Length);

            memStream.Write(bytes, 0, bytes.Length);

            return memStream;
        }

        public string GetJson()
        {
            return JsonSerializer.Serialize(this, this.GetType());
        }

        public static object GetPayload(MemoryStream memoryStream, Type returnType)
        {
            using StreamReader streamReader = new StreamReader(memoryStream, leaveOpen:true);
            string payload = streamReader.ReadToEnd();

            memoryStream.Position = 0;

            return JsonSerializer.Deserialize(payload, returnType) ?? throw new Exception();
        }
    }
}
