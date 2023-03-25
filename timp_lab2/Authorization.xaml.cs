using System.Windows;
using System.Windows.Input;

namespace timp_lab2;


public partial class Authorization : Window
{
    private readonly Window authorization = new MenuWindow();

    public Authorization()
    {
        InitializeComponent();
    }

    private void EntryButton_OnClick(object sender, RoutedEventArgs e)
    {
        authorization.Show();
        Close();
    }

    private void DeclineButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}