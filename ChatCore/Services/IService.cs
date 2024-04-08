using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCore.Services
{
    public interface IService
    {
        public void Handle(ProtocolMessage message);
    }
}
