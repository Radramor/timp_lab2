using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MenuItem;

public class MenuItemViewModel
{
    public string Name { get; set; }
    public bool IsEnabled { get; set; } = true;
    public ICommand Command { get; set; }
    public ObservableCollection<MenuItemViewModel> Children { get; } = new();
}