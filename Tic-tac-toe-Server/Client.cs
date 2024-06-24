using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Tic_tac_toe.Models;

namespace Tic_tac_toe_Server
{
    internal class Client
    {
        public TcpClient ClientSocket { get; set; } 

        public Guid UserId { get; set; }

        public NetworkStream NetworkStream { get; set; }

        public Client(TcpClient client)
        {
            ClientSocket = client;
            UserId = Guid.NewGuid();    

            if(ClientSocket.Connected)
            {
                Console.WriteLine($"User {UserId} has connected.");
            }

            NetworkStream = client.GetStream();
        }

        public Box[] ReadMainGameFieldData()
        {
            byte[] buffer = new byte[1024];
            int bytesRead = NetworkStream.Read(buffer, 0, buffer.Length);
            string json = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Box[] boxCollection = JsonSerializer.Deserialize<Box[]>(json);

            return boxCollection;
        }
    }
}
