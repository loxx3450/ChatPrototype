using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolLibrary.Payload
{
    /// <summary>
    /// Interface <c>IPayload</c> provides base logic of 
    /// presentating Payload as Stream and building it based on Stream, 
    /// and providing string-implementation of his type.
    /// </summary>
    public interface IPayload
    {
        /// <summary>
        /// This method creates a stream that is based on the Payload
        /// </summary>
        /// <returns>
        /// The Stream of Payload.
        /// </returns>
        public MemoryStream GetStream();


        /// <summary>
        /// This static method provides the logic of building Payload 
        /// by having corresponding MemoryStream.
        /// </summary>
        /// <param name="memoryStream">The MemoryStream of Payload.</param>
        /// <param name="returnType">The type of Payload we are recovering to.</param>
        /// <returns>The Payload of type <paramref name="returnType"/> as object.</returns>
        public static abstract object GetPayload(MemoryStream memoryStream, Type returnType);
    }
}
