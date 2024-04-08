using ChatCore.PayloadTypes;
using ProtocolLibrary.Core;
using ProtocolLibrary.Payload;
using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCore.Services
{
    public class AuthorizationService : IService
    {
        public void Handle(ProtocolMessage message)
        {
            AuthRequestPayload payload = PayloadBuilder.GetPayload<AuthRequestPayload>(message.PayloadStream);

            message.Payload = payload;

            Console.WriteLine(payload.Login);
            Console.WriteLine(payload.Password);
        }
    }
}
