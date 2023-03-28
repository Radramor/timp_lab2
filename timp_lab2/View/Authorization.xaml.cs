using System;
using System.Windows;
using timp_lab2.ViewModel;

namespace timp_lab2;


public partial class Authorization
{

    private readonly AuthorizationViewModel _viewModel;
    public Authorization()
    {
        InitializeComponent();
        _viewModel = new AuthorizationViewModel();
    }

    private void TryToEnter(object sender, RoutedEventArgs e)
    {
        try
        {
            var menuDataPath = _viewModel.TryToEnter(LoginBox.Text, PasswordBox.Password);
            
            var menuWindow = new MenuWindow(menuDataPath);
            menuWindow.Show();
            Close();
        }
        catch (ArgumentException)
        {
            MessageBox.Show("Неверное имя пользователя или пароль!");
        }
    }

    private void DeclineButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}