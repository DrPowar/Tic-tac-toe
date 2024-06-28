using System.Collections.ObjectModel;
using System.ComponentModel;
using Tic_tac_toe.Constants;
using Tic_tac_toe.Fabric;
using Tic_tac_toe.Models;
using Tic_tac_toe.Service;
using Tic_tac_toe.WinnerCombination;

namespace Tic_tac_toe.ViewModel
{
    internal partial class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _gameStatusField;
        private EndOfGameChecker _endOfGameChecker;
        public GameHistory GameHistory { get; set; }
        
        public ObservableCollection<Cell> Cells { get; set; }
        public string GameStatusField
        {
            get => _gameStatusField;
            set
            {
                _gameStatusField = value;
                OnPropertyChanged(nameof(GameStatusField));
            }
        }   

        private UserService _userService;
        public Cell[] boxCollection { get; set; }

        public MainWindowViewModel(UserService userService, WinnerCombinationBase winnerCombination, GameHistory gameHistory)
        {
             Cells = new ObservableCollection<Cell>(CellFactory.Build(9, CellType.Cell));
            _userService = userService;
            _endOfGameChecker = new EndOfGameChecker(winnerCombination);
            GameHistory = gameHistory;
            StartNewGame();
        }

        public void StartNewGame()
        {
            boxCollection = new Cell[9];
            for (int i = 0; i < boxCollection.Length; i++)
            {
                boxCollection[i] = new Cell();
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
            boxCollection[int.Parse(param) - 1].BoxSetValues(_userService.CurrentUser.UserSymbol, _userService.CurrentUser.UserSymbolName);
            GameHistory.AddMove(new Move(_userService.CurrentUser, int.Parse(param) - 1));
            ChangeTurn();
        }

        public void ChangeTurn()
        {
            if (!_endOfGameChecker.CheckForWinner(boxCollection))
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
                GameStatusField = GameStatusConst.EndOfGame + " " + _userService.CurrentUser.UserSymbolName;
                return;
            }

            if(_endOfGameChecker.CheckForDraw(boxCollection))
            {
                GameStatusField = GameStatusConst.Draw;
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

            if(CheckForDraw())
            {
                GameStatusField = GameStatusConst.Draw;
                return true;
            }

            return false;
        }

        private bool CheckForDraw()
        {
            byte drawCounter = 0;
            foreach (Cell box in boxCollection)
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
