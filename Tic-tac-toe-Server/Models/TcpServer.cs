using System.Net;
using System.Net.Sockets;
using Tic_tac_toe.Models;
using Tic_tac_toe.Constants;
using Tic_tac_toe.Service;

namespace Tic_tac_toe_Server.Models
{
    internal class TcpServer
    {
        private Client Client1 { get; set; }

        private Client Client2 { get; set; }

        private UserService _userService;

        public TcpListener Listener;

        public string IpAddress { get; set; }

        public int Port { get; set; }

        public TcpServer(string ipAddress, int port, UserService userService)
        {
            Listener = new TcpListener(IPAddress.Parse(ipAddress), port);
            _userService = userService;
        }

        public void StartServer()
        {
            try
            {
                Listener.Start();
                Console.WriteLine($"Server started at {IpAddress}:{Port}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting server: {ex.Message}");
                throw;
            }
        }

        public void StopServer()
        {
            try
            {
                Listener.Stop();
                Console.WriteLine("Server stopped.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error stopping server: {ex.Message}");
                throw;
            }
        }

        public void ConnectsClient()
        {
            try
            {
                TcpClient tcpClient = Listener.AcceptTcpClient();

                if (Client1 == null)
                {
                    Client1 = new Client(tcpClient, _userService.User1);
                    Console.WriteLine($"Client {Client1.ClientId} connected.");
                    Client1.SendUserData();
                }
                else if (Client2 == null)
                {
                    Client2 = new Client(tcpClient, _userService.User2);
                    Console.WriteLine($"Client {Client2.ClientId} connected.");
                    Client2.SendUserData();
                }
                else
                {
                    Console.WriteLine("Two clients are already connected.");
                    tcpClient.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting client: {ex.Message}");
                throw;
            }
        }


        public bool AllClientConnected()
        {
            if (Client1 != null && Client2 != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ClientGameDataModel> GetClientsDataAsync()
        {
            if (AllClientConnected())
            {
                var client1DataTask = Task.Run(() =>
                {
                    ClientGameDataModel data;
                    do
                    {
                        data = Client1.ReadMainGameData();
                    }
                    while (data == null);
                    return data;
                });

                var client2DataTask = Task.Run(() =>
                {
                    ClientGameDataModel data;
                    do
                    {
                        data = Client2.ReadMainGameData();
                    }
                    while (data == null);
                    return data;
                });

                var completedTask = await Task.WhenAny(client1DataTask, client2DataTask);
                return await completedTask;
            }
            return null;
        }


        public void SendClientsData(ServerUserDataModel serverUserData)
        {
            if (AllClientConnected())
            {
                if (serverUserData != null)
                {
                    Client1.SendGameData(serverUserData);
                    Client2.SendGameData(serverUserData);
                }
            }
        }


    }
}
