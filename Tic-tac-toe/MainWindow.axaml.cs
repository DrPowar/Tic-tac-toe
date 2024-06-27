using Avalonia.Controls;
using System;
using System.Diagnostics;
using Tic_tac_toe.Models;
using Tic_tac_toe.Net;
using Tic_tac_toe.Service;
using Tic_tac_toe.ViewModel;

namespace Tic_tac_toe
{
    internal partial class MainWindow : Window
    {
        private UserService _userService;
        private Server _server;
        private User _user;
        private Box[] mainGameField;
        private MainWindowViewModel _viewModel;
        public MainWindow()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            InitializeComponent();

            _userService = new UserService();
            _userService.InitializeUsers();

            _server = new Server();
            _server.ConnectToServer();
            _user = _server.ReceiveUserData();


            _viewModel = new MainWindowViewModel(_userService, _server, _user, new Box[9]);
            DataContext = _viewModel;

            StartListeningToServerData();
        }



        private async void StartListeningToServerData()
        {
            while (true)
            {
                ServerUserDataModel serverUserData = await _server.ReceiveGameDataAsync();
                if (serverUserData != null)
                {
                    mainGameField = serverUserData.Boxes;
                    if (_user.UserSymbolName == serverUserData.CurrentUser.UserSymbolName)
                    {
                        _user.IsActived = true;
                    }
                    Debug.WriteLine($"User {_user.UserSymbolName} received data: {serverUserData}");
                }
                else
                {
                    Debug.WriteLine("Received null data from server.");
                }
            }
        }

    }
}
