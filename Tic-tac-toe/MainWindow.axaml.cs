using Avalonia.Controls;
using System;
using Tic_tac_toe.Service;
using Tic_tac_toe.ViewModel;
using Tic_tac_toe.WinnerCombination;

namespace Tic_tac_toe
{
    internal partial class MainWindow : Window
    {
        private UserService _userService;
        public MainWindow()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            InitializeComponent();
            _userService = new UserService();
            _userService.InitializeUsers();
            StandartWinnerCombination _winnerCombination = new StandartWinnerCombination();
            DataContext = new MainWindowViewModel(_userService, _winnerCombination);
        }
    }
}
