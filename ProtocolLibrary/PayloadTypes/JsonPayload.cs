using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProtocolLibrary.PayloadTypes
{
    public abstract class JsonPayload : IPayload
    {
        public string GetPayloadType()
        {
            return "json";
        }

        public MemoryStream GetStream()
        {
            MemoryStream memStream = new MemoryStream();

            byte[] bytes = Encoding.UTF8.GetBytes(GetJson());

            memStream.Write(bytes, 0, bytes.Length);

            return memStream;
        }

        public string GetJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
