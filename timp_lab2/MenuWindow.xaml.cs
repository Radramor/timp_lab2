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
        
        public MenuWindow(/*string path*/)
        {
            InitializeComponent();
            //_menu.Items.Clear();

            List<string> strings = ReadStringsInMenuFile(/*path*/);

            List<int> levelNumbers = SplitStringsOnNumbers(strings, 0);

            List<string> itemNames = SplitStringsOnWords(strings, 1);

            //List<int> status = SplitStringsOnNumbers(strings, 2);

            //List<string> methodsNames = SplitStringsOnWords(strings, 3);

            AddMenu(itemNames, levelNumbers);

            
        }

        
        
        public List<string> ReadStringsInMenuFile()
        {
            List<string> strings = new List<string>();
            string path = "Data\\1.txt";
            string line;

            using (StreamReader sr = new StreamReader(path, true))
            {
                while (true)
                {
                    line = sr.ReadLine();
                    if (line == null) break;
                    else strings.Add(line);
                }
            }

            return strings;
        }

        public void AddMenu(List<string> names, List<int> LevelNumbers) 
        {
            List<MenuItem> items = new List<MenuItem>();

            for (int i = 0; i < names.Count; i++)
            {
                items.Add(new MenuItem());
                items[i].Header = names[i];
                // if (i > 0 && LevelNumbers[i] > LevelNumbers[i - 1])
                // {
                //     CreateSubitems(items, i, names, LevelNumbers);
                // }

                _menu.Items.Add(items[i]);
            }

            /*items.Add(new MenuItem());
            items[0].Header = names[0];
            items.Add(new MenuItem());
            items[1].Header = names[1];
            items.Add(new MenuItem());
            items[2].Header = names[2];
            items.Add(new MenuItem());
            items[3].Header = names[3];

            items[2].Items.Add(items[3]);
            items[1].Items.Add(items[2]);
            items[0].Items.Add(items[1]);
            _menu.Items.Add(items[0]);*/
        }

        public MenuItem CreateSubitems(List<MenuItem> items, int i, List<string> names, List<int> LevelNumbers)
        {
            items[i - 1].Items.Add(items[i]);
            return items[i];
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
        }
    }
}
