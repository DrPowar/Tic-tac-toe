using Avalonia.Controls;
using System;
using Tic_tac_toe.Service;
using Tic_tac_toe.ViewModel;

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
            DataContext = new MainWindowViewModel(_userService);
        }
    }
}
