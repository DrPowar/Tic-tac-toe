using System;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using Tic_tac_toe.Constants;
using Tic_tac_toe.Models;
using Tic_tac_toe.Net;
using Tic_tac_toe.Service;

namespace Tic_tac_toe.ViewModel
{
    internal partial class MainWindowViewModel : INotifyPropertyChanged
    {
        private Server _server;

        private UserService _userService;

        private GameHistory _gameHistory;
        private User _user;

        public Box[] boxCollection { get; set; }

        private string _serverStatus = "Server not connected";

        private string _gameStatusField;


        public MainWindowViewModel(UserService userService, GameHistory gameHistory, Server server, User user)
        {
            _server = new Server();
            _userService = userService;
            _gameHistory = gameHistory;
            _server = server;
            _user = user;
            StartNewGame();
        }

        public string ServerStatus
        {
            get => _serverStatus;
            set
            {
                _serverStatus = value;
                OnPropertyChanged(nameof(ServerStatus));
            }
        }

        public string GameStatusField
        {
            get => _gameStatusField;
            set
            {
                _gameStatusField = value;
                OnPropertyChanged(nameof(GameStatusField));
            }
        }


        public void ConnectToServer()
        {
            if (_server.IsConnected())
            {
                ServerStatus = "Server connected";
                OnPropertyChanged(nameof(ServerStatus));
            }
        }


        public void StartNewGame()
        {
            boxCollection = new Box[9];
            for (int i = 0; i < boxCollection.Length; i++)
            {
                boxCollection[i] = new Box();
            }
            GameStatusField = GameStatusConst.PlayerTurn + " " + _userService.CurrentUser.UserSymbolName;
        }


        public void RestartGame()
        {
            for (int i = 0; i < boxCollection.Length; i++)
            {
                boxCollection[i].BoxReset();
            }
            _userService.ChangeCurrentUser();
            GameStatusField = GameStatusConst.PlayerTurn + " " + _userService.CurrentUser.UserSymbolName;
        }




        public void BoxClick(string param)
        {
            boxCollection[int.Parse(param) - 1].BoxSetValues(_user.UserSymbol, _user.UserSymbolName);

            ClientGameDataModel gmd = new ClientGameDataModel(boxCollection, new Move(_user, Convert.ToByte(param)));

            _server.SendData(gmd.ToJsonData(gmd));

            ChangeTurn();
        }

        public void ChangeTurn()
        {
            if (!CheckForEndOfGame())
            {
                if (GameStatusField == GameStatusConst.PlayerTurn + " " + SymbolsConst.SymbolX)
                {
                    GameStatusField = GameStatusConst.PlayerTurn + " " + SymbolsConst.SymbolO;
                }
                else
                {
                    GameStatusField = GameStatusConst.PlayerTurn + " " + SymbolsConst.SymbolX;
                }
            }
            else
            {
                return;
            }

            _userService.ChangeCurrentUser();
        }

        public bool CheckForEndOfGame()
        {
            int[][] winningCombinations = new int[][]
            {
                new int[] { 0, 1, 2 },
                new int[] { 3, 4, 5 },
                new int[] { 6, 7, 8 },
                new int[] { 0, 3, 6 },
                new int[] { 1, 4, 7 },
                new int[] { 2, 5, 8 },
                new int[] { 0, 4, 8 },
                new int[] { 2, 4, 6 }
            };

            foreach (var combination in winningCombinations)
            {
                string firstSymbol = boxCollection[combination[0]].SymbolName;
                if (!string.IsNullOrEmpty(firstSymbol) &&
                    firstSymbol == boxCollection[combination[1]].SymbolName &&
                    firstSymbol == boxCollection[combination[2]].SymbolName)
                {
                    GameStatusField = GameStatusConst.EndOfGame + " " + firstSymbol;
                    return true;
                }
            }

            if (CheckForDraw())
            {
                GameStatusField = GameStatusConst.Draw;
                return true;
            }

            return false;
        }

        private bool CheckForDraw()
        {
            byte drawCounter = 0;
            foreach (Box box in boxCollection)
            {
                if (!box.IsEmpty)
                {
                    drawCounter++;
                }
                if (drawCounter == 9)
                {
                    GameStatusField = GameStatusConst.Draw;
                    return true;
                }
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
