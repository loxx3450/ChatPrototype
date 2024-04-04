using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolLibrary.PayloadTypes
{
    public class InfoFromServerPayload : JsonPayload
    {
        public string Message { get; set; }

        public InfoFromServerPayload(string message) 
        { 
            Message = message;
        }
    }
}
