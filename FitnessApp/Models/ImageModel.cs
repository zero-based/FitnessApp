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

        public ImageModel(string imageFilePath)
        {
            _byteArray = ConvertImageFileToByteArray(imageFilePath);
            _source = new BitmapImage(new Uri(imageFilePath, UriKind.RelativeOrAbsolute));
        }

        public string Default { get; set; }

        public byte[] ByteArray
        {
            get  { return _byteArray; }
            set { _byteArray = value; }
        }

        public ImageSource Source
        {
            get
            {
                if (Default == null ) { return _source; }
                else if (_byteArray != null) { return ConvertByteArrayToImageSource(); }
                else { return new BitmapImage(new Uri(Default, UriKind.RelativeOrAbsolute)); }
            }

            set { _source = value; }
        }

       
        // Convert Image to Byte array
        private byte[] ConvertImageFileToByteArray(string imageFilePath)
        {
            FileStream fileSteam = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
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
