using System.Collections.ObjectModel;
using System.Data;
using MenuItem.Domain;
using ReactiveUI;

namespace MenuItem;

public class MenuViewModel : ReactiveObject
{
    public ObservableCollection<MenuItemViewModel> MenuItems { get; }
    private Type _viewType { get; }

    public MenuViewModel(string menuDataPath, Type viewtype)
    {
        _viewType = viewtype;
        MenuItems = new ObservableCollection<MenuItemViewModel>();
        var dataList = DeserializeMenuData.ParseData(string.Concat("Data/", menuDataPath));
        AddMenu(dataList);
    }

    public void AddMenu(List<List<object>> dataList)
    {
        var parentStack = new Stack<MenuItemViewModel>();

        for (var i = 0; i < dataList.Count; i++)
        {
            var level = (int)dataList[i][0];
            var name = (string)dataList[i][1];
            var visibility = (int)dataList[i][2];
            var handler = (string)dataList[i][3];

            if (visibility == 2) continue; // Пропустить невидимые элементы

            var item = new MenuItemViewModel
            {
                Name = name,
                IsEnabled = visibility != 1
            };

            if (handler != "") // If there is a handler, add a click event 
            {
                var method = _viewType.GetMethod(handler);
                if (method == null)
                    throw new DataException($"Handler {handler} not found for {name}");

                item.Command = ReactiveCommand.Create(() => { method.Invoke(null, null); });
            }
            else // Проверить наличие детей при отсутствии обработчика
            {
                if (dataList.Count == i || (int)dataList[i + 1][0] <= level)
                    throw new DataException($"Item {name} must have either a handler or children");
            }

            if (level == 0) // Элемент верхнего уровня
            {
                MenuItems.Add(item);
                parentStack.Push(item);
            }
            else // Подпункт
            {
                if (i != 0)
                {
                    var prevLevel = (int)dataList[i - 1][0];
                    while (prevLevel > level - 1)
                    {
                        prevLevel--;
                        parentStack.Pop();
                    }

                    var parent = parentStack.Peek();
                    parent.Children.Add(item);
                    parentStack.Push(item);
                }
                else
                {
                    throw new DataException($"First item {name} should be first in hierarchy");
                }
            }
        }
    }
}