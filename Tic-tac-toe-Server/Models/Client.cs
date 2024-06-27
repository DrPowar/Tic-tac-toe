using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;
using Tic_tac_toe.Models;

namespace Tic_tac_toe_Server.Models
{
    public class Client
    {
        public TcpClient ClientSocket { get; set; }

        public User User { get; set; }

        public Guid ClientId { get; set; }

        public NetworkStream NetworkStream { get; set; }

        public Client(TcpClient client, User user)
        {
            User = user;
            ClientSocket = client;
            ClientId = Guid.NewGuid();

            if (ClientSocket.Connected)
            {
                Console.WriteLine($"User {ClientId} has connected.");
            }

            NetworkStream = client.GetStream();
        }

        public void SendUserData()
        {
            try
            {
                NetworkStream stream = ClientSocket.GetStream();
                if (stream.CanWrite)
                {
                    string userData = JsonConvert.SerializeObject(User);
                    byte[] userDataBytes = Encoding.UTF8.GetBytes(userData);
                    stream.Write(userDataBytes, 0, userDataBytes.Length);
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка відправки даних клієнту {ClientId}: {ex.Message}");
            }
        }

        public void SendGameData(ServerUserDataModel serverUserData)
        {
            try
            {
                NetworkStream stream = ClientSocket.GetStream();
                if (stream.CanWrite)
                {
                    string serverUserDataJson = JsonConvert.SerializeObject(serverUserData);
                    byte[] serverUserDataBytes = Encoding.UTF8.GetBytes(serverUserDataJson);
                    stream.Write(serverUserDataBytes, 0, serverUserDataBytes.Length);
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка відправки даних клієнту {ClientId}: {ex.Message}");
            }
        }
        public ClientGameDataModel ReadMainGameData()
        {
            byte[] buffer = new byte[1024];
            int bytesRead = NetworkStream.Read(buffer, 0, buffer.Length);
            string json = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            ClientGameDataModel gmd = ClientGameDataModel.FromJsonData(json);

            return gmd;
        }
    }
}
