using System.Windows;
using timp_lab2.ViewModel;

namespace timp_lab2;

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