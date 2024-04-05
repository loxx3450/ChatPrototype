using ProtocolLibrary.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolLibrary.Core
{
    public static class PayloadBuilder
    {
        public static T GetPayload<T>(MemoryStream memoryStream)
            where T : IPayload
        {
            return (T)(T.GetPayload(memoryStream, typeof(T)));
        }
    }
}
