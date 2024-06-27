using Tic_tac_toe.Models;
using Tic_tac_toe.Service;

namespace Tic_tac_toe_Server.Services
{
    internal class GameService
    {
        private UserService _userService;
        private GameHistory _gameHistory;

        public GameService()
        {
            _userService = new UserService();
            _gameHistory = new GameHistory();
        }
    }
}
