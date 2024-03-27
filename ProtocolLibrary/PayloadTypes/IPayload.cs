using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolLibrary.PayloadTypes
{
    public interface IPayload
    {
        public MemoryStream GetStream();
        public string GetPayloadType();
    }
}
