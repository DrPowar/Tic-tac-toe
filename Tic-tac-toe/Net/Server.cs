using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using Tic_tac_toe.Models;

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

                    if (IsConnected())
                    {
                        stream = client.GetStream();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        public User ReceiveUserData()
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string userDataJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(userDataJson);
                user.UpdatUserImage();
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Помилка отримання даних: {ex.Message}");
                return null;
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
