using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace IconExtractor
{
    public partial class MainWindow : Window
    {
        private OpenFileDialog _ddsFile;
        private OpenFileDialog _txtFile;
        private FolderBrowserDialog _folderBrowser;
        private Rectangle _itemIconRect;
        private Rectangle _iconFileRect;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenDds()
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

        private void OpenTxt()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = " |*.txt";
            openFileDialog.Title = "Select a .txt file";
            if (openFileDialog.ShowDialog() == true)
            {
                _txtFile = openFileDialog;
                lblTxtPath.Content = _txtFile.FileName;
            }
        }

        private void SelectOutputFolder()
        {
            FolderBrowserDialog _folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = _folderBrowserDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                _folderBrowser = _folderBrowserDialog;
                lblOutput.Content = _folderBrowser.SelectedPath;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Output folder error!");
            }
        }

        private void ExtractIcons()
        {
            Bitmap bm = _DDS.LoadImage(_ddsFile.FileName);
            bm.Save(_ddsFile.SafeFileName);
            _iconFileRect = new Rectangle(0, 0, bm.Width, bm.Height);

            StreamReader sr = new StreamReader(_txtFile.SafeFileName, Encoding.GetEncoding("GB2312"));

            int tempY = Convert.ToInt32(sr.ReadLine());
            int tempX = Convert.ToInt32(sr.ReadLine());
            _itemIconRect = new Rectangle(0, 0, tempY, tempX);

            tempY = Convert.ToInt32(sr.ReadLine());
            tempX = Convert.ToInt32(sr.ReadLine());
            _iconFileRect = new Rectangle(0, 0, tempX, tempY);

            string line;
            int iconIndex = 0;

            Bitmap bmpImage = new Bitmap(bm);

            while ((line = sr.ReadLine()) != null)
            {
                try
                {
                    Rectangle p = CalculateIconPositionFromDdsFile(iconIndex);
                    bm.Clone(p, bmpImage.PixelFormat).Save($"{_folderBrowser.SelectedPath}/{line.Replace(".dds","")}.png");
                    lbDdsNames.Items.Add(line);
                    iconIndex++;
                }
                catch { }
            }
        }

        private Rectangle CalculateIconPositionFromDdsFile(int iconIndex)
        {
            Rectangle pos = new Rectangle(0, 0, _itemIconRect.Width, _itemIconRect.Height);
            int remainder;
            int quotient = Math.DivRem(iconIndex, _iconFileRect.Width, out remainder);
            pos.X = remainder * _itemIconRect.Width;
            pos.Y = quotient * _itemIconRect.Height;

            return pos;
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenDds();
        }

        private void btnExtract_Click(object sender, RoutedEventArgs e)
        {
            ExtractIcons();
            lblStatus.Content = "Done";
        }

        private void btnOutputFolder_Click(object sender, RoutedEventArgs e)
        {
            SelectOutputFolder();
        }

        private void btnOpenTxt_Click(object sender, RoutedEventArgs e)
        {
            OpenTxt();
        }
    }
}
