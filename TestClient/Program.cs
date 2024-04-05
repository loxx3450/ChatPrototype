using EventSocket;
using EventSocket.Sockets;
using ProtocolLibrary;
using ProtocolLibrary.PayloadTypes;
using ProtocolLibrary.SocketEventMessages;
using System.Net.Sockets;
using System.Text.Json;

const string hostname = "127.0.0.1";
const int port = 8080;

ClientSocketEvent socketEvent = new ClientSocketEvent(hostname, port);

//Waiting for SocketEvent from other side
SocketEvent client = await socketEvent.GetSocketAsync();

//Socket's setup
SetupSocket(client);

ProtocolMessage message = new ProtocolMessage();
message.MessageType = ProtocolMessageType.AuthRequest;

message.SetHeader("testHeader", "testValue");

message.SetPayload(new AuthRequestPayload("vasia", "123123123"));

SocketEventProtocolMessage messageToServer = new SocketEventProtocolMessage("AuthRequest", message);


while (true)
{
    Console.ReadLine();
    client.Emit(messageToServer);
}

void SetupSocket(SocketEvent socket)
{
    //1. Setting supported SocketEventMessages's Types for income
    socket.AddSupportedMessageType<SocketEventProtocolMessage>();

    //2. Setting callbacks
    //...
}