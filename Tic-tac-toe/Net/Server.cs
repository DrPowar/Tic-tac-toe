using System;
using System.Diagnostics;
using System.Net.Sockets;

namespace Tic_tac_toe.Net
{
    internal class Server
    {
        private TcpClient client;

        private NetworkStream stream;

        public Server()
        {
            client = new TcpClient();
        }

        public void SendData(byte[] data)
        {
            stream.Write(data, 0, data.Length);
        }

        public void ConnectToServer()
        {
            if (!client.Connected)
            {
                try
                {
                    client.Connect("127.0.0.1", 7891);

                    if(IsConnected())
                    {
                        stream = client.GetStream();
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        public bool IsConnected()
        {
            if(client.Connected)
            { 
                return true; 
            }
            else
            {
                return false;
            }
        
        }
    }
}
