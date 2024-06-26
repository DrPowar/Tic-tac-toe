using System.Net;
using System.Net.Sockets;
using Tic_tac_toe.Models;
using Tic_tac_toe.Constants;

namespace Tic_tac_toe_Server
{
    internal class TcpServer
    {
        private Client Client1 { get; set; }

        private Client Client2 { get; set; }

        public TcpListener Listener;

        public string IpAddress { get; set; }

        public int Port { get; set; }

        public TcpServer(string ipAddress, int port)
        {
            Listener = new TcpListener(IPAddress.Parse(ipAddress), port);
        }

        public void StartServer()
        {
            try
            {
                Listener.Start();
                Console.WriteLine($"Сервер увімкнутий {IpAddress}:{Port}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка вмикання сервера: {ex.Message}");
                throw;
            }
        }

        public void StopServer()
        {
            try
            {
                Listener.Stop();
                Console.WriteLine($"Сервер зупинено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка вимикання сервера: {ex.Message}");
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
                    Client1 = new Client(tcpClient, new User(SymbolsConst.SymbolX, true));
                    Console.WriteLine($"Клієнт {Client1.ClientId} підключено.");
                    Client1.SendUserData();
                }
                else if (Client2 == null)
                {
                    Client2 = new Client(tcpClient, new User(SymbolsConst.SymbolO, false));
                    Console.WriteLine($"Клієнт {Client1.ClientId} підключено.");
                    Client2.SendUserData();
                }
                else
                {
                    Console.WriteLine("Два клієнти вже підключенні.");
                    tcpClient.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка підключення клаєнта: {ex.Message}");
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

        public void GetClientsData()
        {
            if (AllClientConnected())
            {
                var client1Data = Client1.ReadMainGameData();

                var client2Data = Client2.ReadMainGameData();
            }
        }

    }
}
