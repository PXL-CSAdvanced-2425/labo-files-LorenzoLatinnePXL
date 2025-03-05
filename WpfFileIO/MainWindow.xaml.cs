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
        private const string _fileName = "names.csv";
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
                ListBoxItem firstNameItem = new ListBoxItem();
                firstNameItem.Content = firstName;

                ListBoxItem lastNameItem = new ListBoxItem();
                lastNameItem.Content = lastName;

                firstNameListBox.Items.Add(firstNameItem);
                lastNameListBox.Items.Add(lastNameItem);
                ClearTextBoxes();
            }
        }

        private void readFileButton_Click(object sender, RoutedEventArgs e)
        {
            firstNameListBox.Items.Clear();
            lastNameListBox.Items.Clear();

            using (StreamReader sr = new StreamReader(_fileName))
            {
                while (!sr.EndOfStream)
                {
                    string lineOfText = sr.ReadLine();              // "firstname;lastname"
                    string[] allNames = lineOfText.Split(';');      // [ "firstname", "lastname" ]
                    string firstName = allNames[0];                 // "firstname"
                    string lastName = allNames[1];                  // "lastname"

                    ListBoxItem firstNameItem = new ListBoxItem();
                    firstNameItem.Content = firstName;

                    ListBoxItem lastNameItem = new ListBoxItem();
                    lastNameItem.Content = lastName;

                    firstNameListBox.Items.Add(firstNameItem);
                    lastNameListBox.Items.Add(lastNameItem);
                }
            }
        }

        private void saveFileButton_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fsw = new FileStream(_fileName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fsw))
                {
                    for (int i = 0; i < firstNameListBox.Items.Count; i++)
                    {
                        ListBoxItem firstNameItem = new ListBoxItem();
                        firstNameItem = firstNameListBox.Items[i] as ListBoxItem;

                        ListBoxItem lastNameItem = new ListBoxItem();
                        lastNameItem = lastNameListBox.Items[i] as ListBoxItem;

                        sw.WriteLine($"{firstNameItem.Content};{lastNameItem.Content}");
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
