using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace http_web_server.network
{
    
    internal class Http
    {
        static private string hostIp;
        static private bool initState = false;
        static private Dictionary<string, string> refTree = new Dictionary<string, string>();
        public static void init(string ip)
        {
            Http.hostIp = ip;
            Http.initState = true;

            Http.refTree.Add("/", "C:\\Users\\james\\source\\repos\\http web server\\http web server\\html\\index.html");
            Http.refTree.Add("/index.js", "C:\\Users\\james\\source\\repos\\http web server\\http web server\\html\\index.js");
        }

        public static void run()
        {

            if (initState) { 
                IPHostEntry iPHost = Dns.GetHostEntry(hostIp, AddressFamily.InterNetwork);
                IPAddress addr = iPHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(addr, 8000);

                Socket listener = new Socket(addr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(10);

                    while (true)
                    {
                        Console.WriteLine("Waiting for connection...");
                        Socket clientScoekt = listener.Accept();

                        string request = null;

                        while (true)
                        {
                            byte[] bytes = new byte[1024];
                            int numByte = clientScoekt.Receive(bytes);

                            request += Encoding.ASCII.GetString(bytes, 0, numByte);

                            if (bytes[1023] == 0) break;

                            Console.WriteLine(request);
                        }

                        Console.WriteLine(request);

                        string[] requestLines = request.Split("\n");
                        string[] getLine = requestLines[0].Split(" ");

                        string htmlResponse = "";

                        try
                        {
                            //string[] contents = File.ReadAllLines("../html/index.html");
                            foreach (string line in File.ReadLines(getHandler(getLine[1]))) htmlResponse += line;

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }



                        byte[] message = Encoding.ASCII.GetBytes("HTTP/1.0 200 OK\n\n" + htmlResponse);
                        clientScoekt.Send(message);

                        clientScoekt.Shutdown(SocketShutdown.Both);
                        clientScoekt.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }


        private static string getHandler(string refrence)
        {
            return refTree[refrence];
        }
    }
}
