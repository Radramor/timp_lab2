using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using timp_lab2.Domain;

namespace timp_lab2;

public partial class MenuWindow
{
    private readonly string _menuDataPath;
    
    public MenuWindow(string menuDataPath)
    {
        _menuDataPath = menuDataPath;
        InitializeComponent();

        var dataList = DeserializeMenuData.ParseData(string.Concat("Data/", _menuDataPath));
        AddMenu(dataList);
    }

    private void AddMenu(List<List<object>> dataList)
    {
        var parentStack = new Stack<MenuItem>();
    
        for (var i = 0; i < dataList.Count; i++)
        {
            var level = (int)dataList[i][0];
            var name = (string)dataList[i][1];
            var visibility = (int)dataList[i][2];
            var handler = (string)dataList[i][3];

            if (visibility == 2) continue; // Пропустить невидимые элементы

            var item = new MenuItem
            {
                Header = name
            };

            if (handler != "") // Если есть обработчик, добавить событие клика 
            {
                var method = typeof(MenuWindow).GetMethod(handler);
                if (method == null) 
                    throw new DataException($"Handler {handler} not found for {name}");
                
                // Провертьб есть ли у элемента дети и обработчик одновременно
                if (dataList.Count > i + 1 && (int)dataList[i + 1][0] > level)
                    throw new DataException($"Item {name} cannot have both a handler and children");
                
                // Добавить событие клика
                item.Click += (sender, e) => 
                    { method.Invoke(null, null); };
            }
            else // Проверить наличие детей при отсутствии обработчика
            {
                if (dataList.Count == i || (int)dataList[i + 1][0] <= level)
                    throw new DataException($"Item {name} must have either a handler or children");
            }

            if (visibility == 1) // Если нужно выключить элемент
            {
                item.IsEnabled = false; // Выключить элемент
            }

            if (level == 0) // Элемент верхнего уровня
            {
                _menu.Items.Add(item);
                parentStack.Push(item);
            }
            else // Подпункт
            {
                if (i != 0)
                {
                    var prevLevel = (int)dataList[i - 1][0];
                    while (prevLevel > level-1)
                    {
                        prevLevel--;
                        parentStack.Pop();
                        
                    }
                    var parent = parentStack.Peek();
                    parent.Items.Add(item);
                    parentStack.Push(item);
                }
                else
                {
                    throw new DataException($"First item {name} should be first in hierarchy");
                }
            }
        }
    } 
    
    public static void Stuff()
    {
        MessageBox.Show("Hello world!", "Some caption");
    }
}