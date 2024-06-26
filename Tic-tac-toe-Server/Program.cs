using System.Net;
using System.Net.Sockets;
using Tic_tac_toe.Models;

namespace Tic_tac_toe_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpServer tcpServer = new TcpServer("127.0.0.1", 7891);
            tcpServer.StartServer();

            while(true)
            {
                if(!tcpServer.AllClientConnected())
                {
                    tcpServer.ConnectsClient();
                }

            }
        }
    }
}
