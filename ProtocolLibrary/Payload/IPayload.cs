using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolLibrary.Payload
{
    //Every Payload can be presented as Stream and built based on Stream, and provides string-implementation of his type
    public interface IPayload
    {
        public MemoryStream GetStream();
        public string GetPayloadType();
        public static abstract object GetPayload(MemoryStream memoryStream, Type returnType);
    }
}
