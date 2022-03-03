using System;
using System.Threading.Tasks;

namespace SendToLB2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new WsClient();
            await client.Connect();
            await client.Send(args[0]);
            await client.Receive();
        }
    }
}
