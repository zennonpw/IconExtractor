using Microsoft.Win32;
using System.Windows;

namespace IconExtractor
{
    public partial class MainWindow : Window
    {
        private OpenFileDialog _ddsFile;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "|*.dds";
            openFileDialog.Title = "Select a .dds file";
            if (openFileDialog.ShowDialog() == true)
            {
                _ddsFile = openFileDialog;
                lblStatus.Content = "dds is ready to extract";
                lblFilePath.Content = _ddsFile.FileName;
            }
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

        private void btnExtract_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
