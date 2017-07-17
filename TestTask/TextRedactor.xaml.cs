using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace TestTask
{
    /// <summary>
    /// Interaction logic for TextRedactor.xaml
    /// </summary>
    /// 

    public partial class TextRedactor : Window
    {
        public TextRedactor()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            string line = File.ReadAllText("UserA.txt");

            if (line == string.Empty || line == tbuser.Text)
            {
                if (openFile.ShowDialog() == true)
                {
                    TextRange tr = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

                    using (FileStream fs = File.Open(openFile.FileName, FileMode.Open))
                    {

                        tr.Load(fs, DataFormats.Text);
                        MessageBox.Show("OKey");


                        using (StreamWriter stream = new StreamWriter("UserA.txt", false))
                        {
                            stream.Write(tbuser.Text);
                        }
                        var file1 = new FileInfo(@"FileName.txt");

                        FileStream st1 = file1.Create();
                        st1.Close();

                        using (StreamWriter stream = new StreamWriter("FileName.txt", false))
                        {
                            stream.Write(openFile.FileName);
                        }

                    }

                }

            }
            else
            {
                if (openFile.ShowDialog() == true)
                {
                    TextRange tr = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                    using (FileStream fs = File.Open(openFile.FileName, FileMode.Open))
                    {

                        tr.Load(fs, DataFormats.Text);
                        MessageBox.Show("Тільки для читання", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        var file1 = new FileInfo(@"UserB.txt");

                        FileStream st1 = file1.Create();
                        st1.Close();

                        using (StreamWriter stream = new StreamWriter("UserB.txt", false))
                        {
                            stream.Write(tbuser.Text);
                        }

                    }
                }
            }


            fileName.Text = openFile.FileName;


        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            string line = File.ReadAllText("UserA.txt");
            string line1 = File.ReadAllText("FileName.txt");

            if (line == tbuser.Text || line == string.Empty)
            {

                TextRange documentTextRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                save.FileName = fileName.Text;
                try
                {
                    using (FileStream fs = File.Create(save.FileName))
                    {
                        documentTextRange.Save(fs, DataFormats.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
            else if (fileName.Text == line1)
            {
                MessageBox.Show("Тільки для читання", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else
            {

                TextRange documentTextRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                save.FileName = fileName.Text;
                try
                {
                    using (FileStream fs = File.Create(save.FileName))
                    {
                        documentTextRange.Save(fs, DataFormats.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }

        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (save.ShowDialog() == true)
            {
                TextRange documentTextRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

                using (FileStream fs = File.Create(save.FileName))
                {
                    documentTextRange.Save(fs, DataFormats.Text);


                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            string line = File.ReadAllText("UserA.txt");

            if (line == tbuser.Text)
            {
                tbuser.Text = line;
                using (StreamWriter stream = new StreamWriter("UserB.txt", false))
                {
                    stream.Write(line);
                }
                    using (StreamWriter stream = new StreamWriter("UserA.txt", false))
                {
                    stream.Write(string.Empty);
                }

                
                MessageBox.Show(tbuser.Text);
            }
            else
            {
                using (StreamWriter stream = new StreamWriter("UserB.txt", false))
                {
                    stream.Write(string.Empty);
                }
            }

            Close();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            string line = File.ReadAllText("USerA.txt");
            if (line == string.Empty)
            {
                TextRange tr = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

                using (FileStream fs = File.Open(fileName.Text, FileMode.Open))
                {

                    tr.Load(fs, DataFormats.Text);
                    MessageBox.Show("OKey");

                }

            }
            else
            {
                MessageBox.Show($"User {line} is working");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            string line = File.ReadAllText("UserA.txt");

            if (line == tbuser.Text)
            {
                tbuser.Text = line;
                using (StreamWriter stream = new StreamWriter("UserB.txt", false))
                {
                    stream.Write(line);
                }
                using (StreamWriter stream = new StreamWriter("UserA.txt", false))
                {
                    stream.Write(string.Empty);
                }


                MessageBox.Show(tbuser.Text);

            }
            else
            {
                using (StreamWriter stream = new StreamWriter("UserB.txt", false))
                {
                    stream.Write(string.Empty);
                }

            }
            MessageBoxResult result = MessageBox.Show("Are you shure?", "Confirm", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
