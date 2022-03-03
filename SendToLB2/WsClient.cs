using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SendToLB2
{
    public class WsClient
    {
        private const string url = "ws://192.168.1.190:9425";
        private ClientWebSocket ws;

        public WsClient()
        {
            ws = new ClientWebSocket();
        }

        public async Task Connect()
        {
            while (ws.State != WebSocketState.Open)
            {
                await ws.ConnectAsync(new Uri(url), CancellationToken.None);
                Console.WriteLine("Web socket : " + ws.State);
                Console.WriteLine("Sending connect request...");

                // Send the connect request and wait for the response
                await Send("connect");
                await Receive();
            }
        }

        public async Task Send(string type)
        {
            StringBuilder message = new StringBuilder();

            // We send a connect request
            if (type == "connect")
            {
                //message.Append("type=" + HttpUtility.UrlEncode("authentication") + "&");
                //message.Append("authenticationKey=" + HttpUtility.UrlEncode(authenticationKey));
                var auth = new Authentication();
                message.Append(JsonSerializer.Serialize(auth));
            }
            else
            {
                var button = new TriggerButton();
                button.ButtonId = type;
                message.Append(JsonSerializer.Serialize(button));
            }

            
            Console.WriteLine("Send message : " + message.ToString());
            var sendBuffer = new ArraySegment<Byte>(Encoding.UTF8.GetBytes(message.ToString()));
            await ws.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task Receive()
        {
            ArraySegment<byte> receivedBytes = new ArraySegment<byte>(new byte[1024]);
            WebSocketReceiveResult result = await ws.ReceiveAsync(receivedBytes, CancellationToken.None);
            Console.WriteLine(Encoding.UTF8.GetString(receivedBytes.Array, 0, result.Count));
        }
    }
}
