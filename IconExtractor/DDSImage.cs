/**
 * by Elfe
 * Source: https://forums.crateentertainment.com/t/c-library-gdimagelibrary/31566
 */

using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace IconExtractor
{
    public static class DDSImage
    {
        [IODescription("Turns a byte array of pixels into a bitmap. Requires width and height of image as well.")]
        unsafe public static Bitmap ImageBuild(byte[] Pixel, int Imgwidth, int Imgheight)
        {
            Bitmap bmp = new Bitmap(Imgwidth, Imgheight);

            Rectangle rect = new Rectangle(0, 0, Imgwidth, Imgheight);

            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            byte* ptrbyte = (byte*)ptr;

            fixed (byte* p = Pixel)
            {
                for (var i = 0; i < Pixel.Length; i += 4)
                {
                    ptrbyte[i + 0] = p[i + 0];
                    ptrbyte[i + 1] = p[i + 1];
                    ptrbyte[i + 2] = p[i + 2];
                    ptrbyte[i + 3] = p[i + 3];

                }
            }

            bmp.UnlockBits(bmpData);

            return bmp;
        }

        [IODescription("Converts an integerarray to a bytearray. Used for pixel arrays here.")]
        public static byte[] ConvertIntArrayToByteArray(int[] intarray)
        {
            return intarray.SelectMany(BitConverter.GetBytes).ToArray();
        }

    }

    public static class _DDS
    {
        [IODescription("Loads a .dds file and returns it as a bitmap.")]
        public static Bitmap LoadImage(string DDsFile)
        {
            if (!File.Exists(DDsFile))
            {
                throw new Exception("File: " + DDsFile + " could not be found.");
            }

            byte[] DDSFile = File.ReadAllBytes(DDsFile);

            DDSReader dds = new DDSReader();

            int[] Pixel = dds.read(DDSFile, dds.ARGB, 0);

            int ImageWidth = dds.getWidth(DDSFile);
            int ImageHeight = dds.getHeight(DDSFile);

            byte[] PixelArray = Pixel.SelectMany(BitConverter.GetBytes).ToArray();

            return DDSImage.ImageBuild(PixelArray, ImageWidth, ImageHeight);
        }

        [IODescription("Loads a .dds file and returns it as a bitmap.")]
        public static Bitmap LoadImage(byte[] FileContent)
        {

            DDSReader dds = new DDSReader();

            int[] Pixel = dds.read(FileContent, dds.ARGB, 0);

            int ImageWidth = dds.getWidth(FileContent);
            int ImageHeight = dds.getHeight(FileContent);

            byte[] PixelArray = Pixel.SelectMany(BitConverter.GetBytes).ToArray();

            return DDSImage.ImageBuild(PixelArray, ImageWidth, ImageHeight);
        }

        [IODescription("Returns the width of a .dds file.")]
        public static int GetFileWidth(string DDsfile)
        {
            if (!File.Exists(DDsfile))
            {
                throw new FileNotFoundException("File: " + DDsfile + " could not be found.");
            }

            DDSReader ddsr = new DDSReader();

            return ddsr.getWidth(File.ReadAllBytes(DDsfile));
        }

        [IODescription("Returns the width of a .dds file.")]
        public static int GetFileWidth(byte[] Content)
        {
            DDSReader ddsr = new DDSReader();

            return ddsr.getWidth(Content);
        }

        [IODescription("Returns the height of a .dds file.")]
        public static int GetFileHeight(string DDsfile)
        {
            if (!File.Exists(DDsfile))
            {
                throw new FileNotFoundException("File: " + DDsfile + " could not be found.");
            }

            DDSReader ddsr = new DDSReader();

            return ddsr.getHeight(File.ReadAllBytes(DDsfile));
        }

        [IODescription("Returns the height of a .dds file.")]
        public static int GetFileHeight(byte[] Content)
        {
            DDSReader ddsr = new DDSReader();

            return ddsr.getHeight(Content);
        }
    }
}
