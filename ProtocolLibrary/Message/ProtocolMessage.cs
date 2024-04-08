using ProtocolLibrary.Core;
using ProtocolLibrary.Payload;

namespace ProtocolLibrary.Message
{
    public class ProtocolMessage
    {
        private const char HEADER_LINE_SEPARATOR = ':';
        private const string HEADER_PAYLOAD_LEN = "p_len";
        private const string HEADER_PAYLOAD_TYPE = "p_type";

        public ProtocolMessageType MessageType { get; set; }

        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        public IPayload? Payload { get; set; }
        public MemoryStream? PayloadStream { get; set; }

        public int PayloadLength
        {
            get
            {
                string? value;

                if (Headers.TryGetValue(HEADER_PAYLOAD_LEN, out value))
                    return Convert.ToInt32(value);

                return 0;
            }
        }

        public string? PayloadType
        {
            get
            {
                string? value;

                if (Headers.TryGetValue(HEADER_PAYLOAD_TYPE, out value))
                    return value;

                return null;
            }
        }

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

        public MemoryStream GetStream()
        {
            return ProtocolMessageStreamBuilder.GetStream(this);
        }
    }
}
