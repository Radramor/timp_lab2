using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace timp_lab2
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        
        public MenuWindow()
        {
            InitializeComponent();
        }

        public List<MenuItem> items = new List<MenuItem>();
        
        public List<string> ReadStringsInMenuFile()
        {
            List<string> names = new List<string>();
            string path = "1.txt";
            string line;

            using (StreamReader sr = new StreamReader(path, true))
            {
                while (true)
                {
                    line = sr.ReadLine();
                    if (line == null) break;
                    else names.Add(line);
                }
            }

            return names;
        }

        public void AddMenu(List<string> names) 
        {
            for (int i = 0; i < names.Count; i++)
            {
                items.Add(new MenuItem());
                items[i].Header = names[i];
                _menu.Items.Add(items[i]);
            }
        }

        public List<string> SplitStringsOnWords(List<string> strings, int N)
        {
            List<string> words = new List<string>();
            string[] wordsInStr;
            foreach (string str in strings)
            {
                wordsInStr = str.Split(" ");
                words.Add(wordsInStr[N]);
            }
            return words;
        }

        public List<int> SplitStringsOnNumbers(List<string> strings, int N)
        {
            string[] wordsInStr;
            List<int> LevelNumbers = new List<int>();
            foreach (string str in strings)
            {
                wordsInStr = str.Split(" ");
                LevelNumbers.Add(int.Parse(wordsInStr[N]));
            }
            return LevelNumbers;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _menu.Items.Clear();

            List<string> strings = ReadStringsInMenuFile();

            MenuItem item = new MenuItem();
            item.Header = "Опция";
            MenuItem item2 = new MenuItem();
            item2.Header = "";
            MenuItem item3 = new MenuItem();
            item3.Header = names[2];

            List<int> LevelNumbers = SplitStringsOnNumbers(strings, 0);

            AddMenu(itemNames);
        }
    }
}
