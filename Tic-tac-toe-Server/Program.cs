using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Tic_tac_toe.Models;

namespace Tic_tac_toe_Server
{
    internal class Program
    {
        static List<Client> clients;
        static TcpListener listener;    
        static void Main(string[] args)
        {
            clients = new List<Client>();
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);
            listener.Start();

            while(true)
            {
                if(clients.Count < 2)
                {
                    var client = new Client(listener.AcceptTcpClient());
                    clients.Add(client);
                }

                Box[] boxes = clients[0].ReadMainGameFieldData();
            }
        }
    }
}
