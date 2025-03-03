using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfFileIO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = firstNameTextBox.Text;
            string lastName = lastNameTextBox.Text;

            if (!String.IsNullOrEmpty(firstName) && !String.IsNullOrEmpty(lastName))
            {
                firstNameListBox.Items.Add(firstName);
                lastNameListBox.Items.Add(lastName);
                ClearTextBoxes();
            }
        }

        private void readFileButton_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fsr = new FileStream("names.txt", FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fsr))
                {
                    string name = sr.ReadToEnd();
                    string[] changedNames = name.Split(' ');
                    foreach (string fullName in changedNames)
                    {
                        Console.WriteLine(fullName);
                    }
                }
            }
        }

        private void saveFileButton_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fsw = new FileStream("names.txt", FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fsw))
                {
                    for (int i = 0; i < firstNameListBox.Items.Count; i++)
                    {
                        sw.Write(firstNameListBox.Items[i]);
                        sw.Write(" ");
                        sw.WriteLine(lastNameListBox.Items[i]);
                    }
                }
            }
        }

        private void ClearTextBoxes()
        {
            firstNameTextBox.Clear();
            lastNameTextBox.Clear();
        }
    }
}
