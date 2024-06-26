using Avalonia.Controls;
using System;
using Tic_tac_toe.Models;
using Tic_tac_toe.Net;
using Tic_tac_toe.Service;
using Tic_tac_toe.ViewModel;

namespace Tic_tac_toe
{
    internal partial class MainWindow : Window
    {
        private UserService _userService;
        private GameHistory _gameHistory;
        private Server _server;
        private User _user;
        public MainWindow()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            InitializeComponent();
            _userService = new UserService();
            _userService.InitializeUsers();
            _server = new Server();
            _server.ConnectToServer();
            if(_user == null)
            {
                _user = _server.ReceiveUserData();
            }
            DataContext = new MainWindowViewModel(_userService, _gameHistory, _server);
        }
    }
}
