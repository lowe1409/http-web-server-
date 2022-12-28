using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace http_web_server.network
{
    internal class ClientChatHandler
    {
        public const int HEADER_LENGTH = 2;

        public static List<ClientChatHandler> handlers = new List<ClientChatHandler>();
        private Socket clientSocket;
        public ClientChatHandler(Socket clientSocket) {
            this.clientSocket = clientSocket;
            handlers.Add(this);
            Thread listenThread = new Thread(run);
            listenThread.Start();
        }


        public Socket getClientSocket()
        {
            return clientSocket;
        }


        private void run()
        {
            byte[] buffer = new byte[2048];
            int usernameLength;
            int messageLength;
            string username;
            string message;

            while (clientSocket.Connected)
            {
                Console.Beep();
                clientSocket.Receive(buffer);
                //getting the lenght of the username and message
                usernameLength = (int)buffer[0] << 8 | (int)buffer[1];
                messageLength = (int)buffer[2] << 8 | (int)buffer[3];

                foreach(ClientChatHandler handler in handlers) {
                    if (!handler.Equals(this))
                    {
                        handler.getClientSocket().Send(buffer);
                    }
                }
            }
        }
    }
}
