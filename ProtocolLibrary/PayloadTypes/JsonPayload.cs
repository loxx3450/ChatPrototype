using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProtocolLibrary.PayloadTypes
{
    public class JsonPayload : IPayload
    {
        public string GetPayloadType()
        {
            return "json";
        }

        public MemoryStream GetStream()         //TODO: capacity
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
    }
}
