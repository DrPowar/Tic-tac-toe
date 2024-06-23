using System;
using System.Diagnostics;
using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GalaSoft.MvvmLight;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualBasic;
using ReactiveUI;
using Tic_tac_toe.Models;
using Tic_tac_toe.Service;

namespace Tic_tac_toe.ViewModel
{
    internal partial class MainWindowViewModel : ViewModelBase
    {
        public string gameStatusField { get; set; } = string.Empty;
        private UserService _userService;
        public Box[] boxCollection { get; set; }
        public MainWindowViewModel(UserService userService)
        {
            _userService = userService;
            boxCollection = new Box[9];
            for (int i = 0; i < boxCollection.Length; i++)
            {
                boxCollection[i] = new Box();
            }
        }

        public void BoxClick(string param)
        {
            boxCollection[int.Parse(param) - 1].BoxImage = _userService.CurrentUser.UserSymbol;
            boxCollection[int.Parse(param) - 1].SymbolName = _userService.CurrentUser.UserSymbolName;
            EndOfGame();
            _userService.ChangeCurrentUser();
        }

        public bool EndOfGame()
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
                    Debug.WriteLine($"Winner: {firstSymbol}");
                    return true;
                }

                byte drawCounter = 0;
                foreach(Box box in boxCollection)
                {
                    if(box != null)
                    {
                        drawCounter++;
                    }
                    if(drawCounter == 9)
                    {
                        Debug.WriteLine("Draw");
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
