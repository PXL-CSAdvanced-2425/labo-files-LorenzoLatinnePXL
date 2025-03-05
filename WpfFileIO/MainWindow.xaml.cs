using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

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

            // using Microsoft.Win32;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = _fileName;
            ofd.InitialDirectory = Environment.CurrentDirectory;

            if (ofd.ShowDialog() == true) // als true, dan geldig bestand in ofd
            {
                string fileNameFromOFD = ofd.FileName;

                using (StreamReader sr = new StreamReader(fileNameFromOFD))
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
        }

        private void saveFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                FileName = _fileName,
                InitialDirectory = Environment.CurrentDirectory
            };

            if (sfd.ShowDialog() == true)
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
        }

        private void ClearTextBoxes()
        {
            firstNameTextBox.Clear();
            lastNameTextBox.Clear();
        }
    }
}
