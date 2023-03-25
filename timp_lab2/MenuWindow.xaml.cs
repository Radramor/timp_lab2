using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace timp_lab2;

public partial class MenuWindow : Window
{
    public MenuWindow( /*string path*/)
    {
        InitializeComponent();
        
        List<List<object>> dataList = ParseData("Data/Data.txt");
        AddMenu(dataList);
    }

    private List<List<object>> ParseData(string filePath)
    {
        // Считывание данных из файла
        var lines = File.ReadAllLines(filePath);

        // Разбирает каждую строку и добавляет её в List
        return (from line in lines
            select Regex.Match(line, @"(\d+)\s+(.+)\s+(\d+)\s*(.*)")
            into match
            where match.Success
                let num1 = int.Parse(match.Groups[1].Value)
                let str1 = match.Groups[2].Value
                let num2 = int.Parse(match.Groups[3].Value)
                let str2 = match.Groups[4].Value
            select new List<object> { num1, str1, num2, str2 }).ToList();
    }

    public void AddMenu(List<List<object>> dataList)
    {
        var parents = new Dictionary<int, MenuItem>();

        foreach (var data in dataList)
        {
            var level = (int)data[0];
            var name = (string)data[1];
            var visibility = (int)data[2];
            var handler = (string)data[3];

            if (visibility == 2) continue; // Пропустить невидимые элементы

            var item = new MenuItem();
            item.Header = name;

            if (handler != "") // Если есть обработчик, добавить событие клика TODO: если там обработчик и дальше есть ниже иерархия, то кинуть ошибку
                item.Click += (sender, e) =>
                {
                    MethodInfo? method = typeof(MenuWindow).GetMethod(handler); //TODO: можно попарсить ошибки в data.txt
                    method?.Invoke(null, null); //TODO: if (method == null) {
                };

            if (visibility == 1) // Если нужно выключить элемент
                item.IsEnabled = false; // Выключить элемент

            if (level == 0) // Элемент верхнего уровня
            {
                _menu.Items.Add(item);
                parents[level] = item;
            }
            else // Подпункт
            {
                var parent = parents[level - 1];
                parent.Items.Add(item);
                parents[level] = item;
            }
        }
    }

    public static void Stuff()
    {
        MessageBox.Show("Hello world!", "Fuck");
    }
}