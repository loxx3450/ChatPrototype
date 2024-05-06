using ProtocolLibrary.Core;
using ProtocolLibrary.Payload;

namespace ProtocolLibrary.Message
{
    /// <summary>
    /// Class <c>ProtocolMessage</c> models a message with headers and payload(optional), 
    /// that could be represented as MemoryStream.
    /// </summary>
    public class ProtocolMessage
    {
        //Service info
        private const char HEADER_LINE_SEPARATOR = ':';
        private const string HEADER_PAYLOAD_LEN = "p_len";
        private const string HEADER_PAYLOAD_TYPE = "p_type";


        //
        // ========== public properties: ==========
        //

        /// <summary>
        /// Headers of message as a key-value-pair.
        /// </summary>
        public Dictionary<string, string> Headers { get; set; } = [];


        /// <summary>
        /// Stream of the Payload. Could be null if 
        /// message doesn't contain payload till now.
        /// </summary>
        public MemoryStream? PayloadStream { get; set; }


        /// <summary>
        /// Payload of Message. Could be recovered by PayloadStream.
        /// Could be <c>null</c>.
        /// </summary>
        public IPayload? Payload { get; set; }


        /// <summary>
        /// Tries to get length from headers. 
        /// In case of success, returns this as int. Otherwise, returns 0;
        /// </summary>
        public int PayloadLength
        {
            get
            {
                if (Headers.TryGetValue(HEADER_PAYLOAD_LEN, out string? value))
                    return Convert.ToInt32(value);

                return 0;
            }
        }


        /// <summary>
        /// Tries to get payload's type from headers. 
        /// In case of success, returns this as string. Otherwise, returns null;
        /// </summary>
        public string? PayloadType
        {
            get
            {
                if (Headers.TryGetValue(HEADER_PAYLOAD_TYPE, out string? value))
                    return value;

                return null;
            }
        }


        //
        // ========== public methods: ==========
        //

        /// <summary>
        /// This method sets the Payload of the Message, creates Payload's stream 
        /// and writes some default headers
        /// </summary>
        public void SetPayload(IPayload payload)
        {
            Payload = payload;

            PayloadStream = payload.GetStream();

            Headers[HEADER_PAYLOAD_LEN] = PayloadStream.Length.ToString();
            Headers[HEADER_PAYLOAD_TYPE] = Payload.GetPayloadType();
        }


        /// <summary>
        /// This method writes header basing on key and value.
        /// </summary>
        public void SetHeader(string key, string value)
        {
            Headers[key] = value;
        }


        /// <summary>
        /// This method writes header basing on the whole header line.
        /// Trimmes the line as well.
        /// </summary>
        public void SetHeader(string headerLine)
        {
            string[] strings = headerLine.Split(HEADER_LINE_SEPARATOR, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            SetHeader(strings[0], strings[1]);
        }


        /// <summary>
        /// This methods returns Stream of the whole Message including headers and payload.
        /// </summary>
        /// <returns>Stream of the Message.</returns>
        public MemoryStream GetStream()
        {
            return ProtocolMessageStreamBuilder.GetStream(this);
        }
    }
}
