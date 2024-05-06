using ProtocolLibrary.Payload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolLibrary.Core
{
    /// <summary>
    /// Class <c>PayloadBuilder</c> provides logic of building inheritors of <c>IPayload</c>.
    /// </summary>
    public static class PayloadBuilder
    {
        /// <summary>
        /// This method returns, builded by MemoryStream, payload of the type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type T should be an inheritor of IPayload.</typeparam>
        /// <returns>The object of IPayload's inheritor.</returns>
        public static T GetPayload<T>(MemoryStream memoryStream)
            where T : IPayload
        {
            Type returnType = typeof(T);

            object payload = T.GetPayload(memoryStream, returnType);

            return (T)payload;
        }
    }
}
