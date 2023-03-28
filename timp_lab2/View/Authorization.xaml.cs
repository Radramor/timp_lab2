using System;
using System.Windows;

namespace timp_lab2;


public partial class Authorization
{
    private readonly Window _authorization = new MenuWindow();

    public Authorization()
    {
        InitializeComponent();
        _viewModel = new LoginViewModel();
    }

    private void TryToEnter(object sender, RoutedEventArgs e)
    {
        try
        {
            _viewModel.TryToEnter(LoginBox.Text, PasswordBox.Password);
            Close();
        }
        catch (ArgumentException)
        {
            MessageBox.Show("Неверное имя пользователя или пароль!");
        }
        _authorization.Show();
        Close();
    }

    private void DeclineButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}