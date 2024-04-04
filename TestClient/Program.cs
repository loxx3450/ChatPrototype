using EventSocket;
using EventSocket.Sockets;
using ProtocolLibrary;
using ProtocolLibrary.PayloadTypes;
using ProtocolLibrary.SocketEventMessages;
using System.Net.Sockets;

const string hostname = "127.0.0.1";
const int port = 8080;

ClientSocketEvent socketEvent = new ClientSocketEvent(hostname, port);

//Waiting for SocketEvent from other side
SocketEvent client = await socketEvent.GetSocketAsync();

//Socket's setup
//....

ProtocolMessage message = new ProtocolMessage();
message.MessageType = ProtocolMessageType.AuthRequest;

message.SetHeader("testHeader", "testValue");

message.SetPayload(new AuthRequestPayload("vasia", "123123123"));

SocketEventProtocolMessage messageToServer = new SocketEventProtocolMessage("MessageToServer", message);


while (true)
{
    Console.ReadLine();
    client.Emit(messageToServer);
}