using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace IconExtractor
{
    public partial class MainWindow : Window
    {
        private OpenFileDialog _ddsFile;
        private FolderBrowserDialog folderBrowser;
        private Rectangle itemIconRect;
        private Rectangle iconFileRect;

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

        private void ExtractIcons()
        {
            Bitmap bm = _DDS.LoadImage(_ddsFile.FileName);
            bm.Save(_ddsFile.SafeFileName);
            iconFileRect = new Rectangle(0, 0, bm.Width, bm.Height);

            StreamReader sr = new StreamReader($"{_ddsFile.SafeFileName.Replace(".dds",".txt")}", Encoding.GetEncoding("GB2312"));

            int tempY = Convert.ToInt32(sr.ReadLine());
            int tempX = Convert.ToInt32(sr.ReadLine());
            itemIconRect = new Rectangle(0, 0, tempY, tempX);

            tempY = Convert.ToInt32(sr.ReadLine());
            tempX = Convert.ToInt32(sr.ReadLine());
            iconFileRect = new Rectangle(0, 0, tempX, tempY);

            string line;
            int iconIndex = 0;

            Bitmap bmpImage = new Bitmap(bm);

            while ((line = sr.ReadLine()) != null)
            {
                try
                {
                    Rectangle p = CalculateIconPositionFromDdsFile(iconIndex);
                    bm.Clone(p, bmpImage.PixelFormat).Save($"{folderBrowser.SelectedPath}/{line.Replace(".dds","")}.png");
                    iconIndex++;
                }
                catch { }
            }
        }

        private Rectangle CalculateIconPositionFromDdsFile(int iconIndex)
        {
            Rectangle pos = new Rectangle(0, 0, itemIconRect.Width, itemIconRect.Height);
            int remainder;
            int quotient = Math.DivRem(iconIndex, iconFileRect.Width, out remainder);
            pos.X = remainder * itemIconRect.Width;
            pos.Y = quotient * itemIconRect.Height;

            return pos;
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

        private void btnExtract_Click(object sender, RoutedEventArgs e)
        {
            ExtractIcons();
        }

        private void btnOutputFolder_Click(object sender, RoutedEventArgs e)
        {
            SelectOutputFolder();
        }

        private void btnOpenTxt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
