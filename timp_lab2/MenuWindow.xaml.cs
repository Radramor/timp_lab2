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

        public List<string> names = new List<string>();
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string path = "1.txt";
            /*using(StreamReader sr = new StreamReader(path, true)
            {
                string line = sr.ReadLine();
                while (line != null)
                {                   
                    names.Add(line);
                    line = sr.ReadLine();
                }
            }*/

            MenuItem item = new MenuItem();
            item.Header = "Опция";
            MenuItem item2 = new MenuItem();
            item2.Header = ";
            MenuItem item3 = new MenuItem();
            item3.Header = names[2];

            item2.Items.Add(item3);
            item.Items.Add(item2);
        
            _menu.Items.Add(item);
            //item.Items.Add(item.Header);
            //_menu.File;
        }
    }
}
