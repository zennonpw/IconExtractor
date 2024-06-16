using System;
using System.Windows;
using System.Windows.Forms;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace IconExtractor
{
    public partial class MainWindow : Window
    {
        private OpenFileDialog _ddsFile;
        private FolderBrowserDialog folderBrowser;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = " |*.dds";
            openFileDialog.Title = "Select a .dds file";
            if (openFileDialog.ShowDialog() == true)
            {
                _ddsFile = openFileDialog;
                lblStatus.Content = "dds is ready to extract";
                lblFilePath.Content = _ddsFile.FileName;
            }
        }

        private void SelectOutputFolder()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folderBrowser = folderBrowserDialog;
                lblOutput.Content = folderBrowser.SelectedPath;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Output folder error!");
            }
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

        private void btnExtract_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOutputFolder_Click(object sender, RoutedEventArgs e)
        {
            SelectOutputFolder();
        }
    }
}
