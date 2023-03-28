using System.Windows;
using MenuItem;

namespace timp_lab2.View;

public partial class MenuWindow
{
    private readonly MenuViewModel _viewModel;

    public MenuWindow(MenuViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
    }

    public static void Stuff()
    {
        MessageBox.Show("Hello world!", "Some caption");
    }

    public static void About()
    {
        MessageBox.Show("About Page!", "About");
    }
}