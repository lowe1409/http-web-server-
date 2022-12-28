using System.Net;
using System.Net.Sockets;
using System.Text;
using http_web_server.network;

namespace http_web_server.main
{
    internal class Program

    {
        const string SERVER_IP = "192.168.0.42";

        static void Main(string[] args)
        {
            Console.Title = "Server";
            
            Http.init(SERVER_IP);
            ClientChat.init(SERVER_IP);

            Thread chatThread = new Thread(ClientChat.run);
            Thread httpThread = new Thread(Http.run);
            httpThread.Start();
            chatThread.Start();
        }
    }
}