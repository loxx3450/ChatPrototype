using ProtocolLibrary;
using ProtocolLibrary.PayloadTypes;
using System.Net.Sockets;

const string host = "127.0.0.1";
const int port = 8080;

using TcpClient tcpClient = new TcpClient(host, port);
using NetworkStream netStrteam = tcpClient.GetStream();

ProtocolMessage message = new ProtocolMessage();
message.MessageType = ProtocolMessageType.AuthRequest;

message.SetHeader("testHeader", "testValue");

message.SetPayload(new AuthRequestPayload("vasia", "123123123"));

while(true)
{
    Thread.Sleep(2000);
    message.GetStream().CopyTo(netStrteam);
}