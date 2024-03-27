using ProtocolLibrary.PayloadTypes;

namespace ProtocolLibrary
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
            MemoryStream memStream = new MemoryStream();
            memStream.Write(new byte[4], 0, 4);

            StreamWriter writer = new StreamWriter(memStream);

            writer.WriteLine(GetMessageType());
            foreach (var header in Headers)
                writer.WriteLine($"{header.Key}:{header.Value}");
            writer.WriteLine();
            writer.Flush();

            if (Payload is not null && PayloadStream is not null)
            {
                PayloadStream.Position = 0;
                PayloadStream.CopyTo(memStream);

                PayloadStream.Position = 0;
            }

            memStream.Position = 0;

            byte[] messageSize = ConvertIntToBytes((int)memStream.Length - 4);

            memStream.Write(messageSize, 0, 4);

            memStream.Position = 0;

            return memStream;
        }

        private string GetMessageType()
        {
            return MessageType switch
            {
                ProtocolMessageType.AuthRequest => "AuthRequest",
                ProtocolMessageType.AuthResponse => "AuthResponse",
                ProtocolMessageType.None => "None"
            };
        }

        private byte[] ConvertIntToBytes(int val)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return bytes;
        }
    }
}
