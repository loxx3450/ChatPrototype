using ProtocolLibrary.Core;
using ProtocolLibrary.Payload;

namespace ProtocolLibrary.Message
{
    public class ProtocolMessage
    {
        //Service info
        private const char HEADER_LINE_SEPARATOR = ':';
        private const string HEADER_PAYLOAD_LEN = "p_len";
        private const string HEADER_PAYLOAD_TYPE = "p_type";


        //Identificator of Message
        public ProtocolMessageType MessageType { get; set; }


        //Headers of Message
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();


        //Payload of Message
        public MemoryStream? PayloadStream { get; set; }

        //Can be recoreved by Stream
        public IPayload? Payload { get; set; }


        public int PayloadLength
        {
            get
            {
                if (Headers.TryGetValue(HEADER_PAYLOAD_LEN, out string? value))
                    return Convert.ToInt32(value);

                return 0;
            }
        }


        public string? PayloadType
        {
            get
            {
                if (Headers.TryGetValue(HEADER_PAYLOAD_TYPE, out string? value))
                    return value;

                return null;
            }
        }


        //Setting basic Headers for Payload
        public void SetPayload(IPayload payload)
        {
            Payload = payload;

            PayloadStream = payload.GetStream();

            Headers[HEADER_PAYLOAD_LEN] = PayloadStream.Length.ToString();
            Headers[HEADER_PAYLOAD_TYPE] = Payload.GetPayloadType();
        }


        public void SetHeader(string key, string value)
        {
            Headers[key] = value;
        }


        public void SetHeader(string headerLine)
        {
            string[] strings = headerLine.Split(HEADER_LINE_SEPARATOR, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            SetHeader(strings[0], strings[1]);
        }


        //Stream of the whole Message
        public MemoryStream GetStream()
        {
            return ProtocolMessageStreamBuilder.GetStream(this);
        }
    }
}
