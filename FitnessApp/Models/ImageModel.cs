using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FitnessApp.Models
{
    public class ImageModel
    {
        private byte[] _byteArray = null;
        private ImageSource _source;

        public ImageModel() { }

        public string FilePath { get; set; }

        public byte[] ByteArray
        {
            get
            {
                if (FilePath == null) { return _byteArray; }
                else { return ConvertImageFileToByteArray(); }
            }

            set { _byteArray = value; }
        }

        public ImageSource Source
        {
            get
            {
                if (FilePath == null) { return ConvertByteArrayToImageSource(); }
                else { return new BitmapImage(new Uri(FilePath, UriKind.RelativeOrAbsolute)); }
            }

            set { _source = value; }
        }

       
        // Convert Image to Byte array
        private byte[] ConvertImageFileToByteArray()
        {
            FileStream fileSteam = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileSteam);

            return binaryReader.ReadBytes((int)fileSteam.Length);
        }


        // Convert Byte array to Image source
        private ImageSource ConvertByteArrayToImageSource()
        {
            BitmapImage bitmapImage = new BitmapImage();
            MemoryStream memoryStream = new MemoryStream(_byteArray);

            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();

            return bitmapImage as ImageSource;
        }

    }
}
