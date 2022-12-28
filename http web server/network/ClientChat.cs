using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace http_web_server.network
{
    internal class ClientChat
    {
        private static bool initState = false;
        private static string hostIp;
        public static void init(string ip)
        {
            initState = true;
            hostIp = ip;
        }

        public static void run()
        {
            if (initState)
            {
                IPHostEntry iPHost = Dns.GetHostEntry(hostIp, AddressFamily.InterNetwork);
                IPAddress addr = iPHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(addr, 12345);

                Socket listener = new Socket(addr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(10);

                    while (true)
                    {
                        Socket newClient = listener.Accept();
                        ClientChatHandler newClientHandler = new ClientChatHandler(newClient);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
