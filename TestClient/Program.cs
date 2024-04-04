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

SocketEventProtocolMessage messageToServer = new SocketEventProtocolMessage("MessageToServer", message);


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
    socket.On("ResponseToClient", resp =>
    {
        ProtocolMessage message = (ProtocolMessage)resp;

        //TEST CODE
        #region printMessage
        Console.WriteLine(message.MessageType.ToString());

        Console.WriteLine("Headers:");
        foreach (var item in message.Headers)
        {
            Console.WriteLine(item.Key + ':' + item.Value);
        }
        Console.WriteLine();

        using (StreamReader reader = new StreamReader(message.PayloadStream, leaveOpen: true))
        {
            string json = reader.ReadToEnd(); // Read JSON from the stream
            InfoFromServerPayload? p = JsonSerializer.Deserialize<InfoFromServerPayload>(json);

            Console.WriteLine(p.Message);
        }
        #endregion
    });
}