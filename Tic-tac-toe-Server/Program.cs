using Tic_tac_toe.Models;
using Tic_tac_toe.Service;
using Tic_tac_toe_Server.Models;
using Tic_tac_toe_Server.Utilities;

namespace Tic_tac_toe_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserService userService = new UserService();
            userService.InitializeUsersWithoutImage();


            TcpServer tcpServer = new TcpServer("127.0.0.1", 7891, userService);
            tcpServer.StartServer();


            GameHistory gameHistory = new GameHistory();

            Cell[] mainGameField = new Cell[9];

            while (true)
            {
                if (!tcpServer.AllClientConnected())
                {
                    tcpServer.ConnectsClient();
                }

                ClientGameDataModel clientGameData = tcpServer.GetClientsDataAsync().GetAwaiter().GetResult();
                if (clientGameData != null)
                {
                    ClientDataParser.ParseClientData(clientGameData, ref gameHistory, ref mainGameField);
                    userService.ChangeCurrentUser();
                    tcpServer.SendClientsData(new Models.ServerUserDataModel(userService.CurrentUser, mainGameField));
                }
            }
        }
    }
}
