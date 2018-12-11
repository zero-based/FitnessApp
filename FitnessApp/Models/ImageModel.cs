using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FitnessApp.Models
{
    public class ImageModel
    {
        private byte[] _byteArray;
        private ImageSource _source;

        public ImageModel() { }

        public string FilePath { get; set; }

        public byte[] ByteArray
        {
            get
            {
                if (FilePath == null) return _byteArray;
                else return ConvertImageFileToByteArray();
            }

            set { _byteArray = value; }
        }

        public ImageSource Source
        {
            get { return ConvertByteArrayToImageSource(); }
            set { _source = value; }
        }

       
        // Convert Image to Byte array
        private byte[] ConvertImageFileToByteArray()
        {
            if (FilePath == null) return _byteArray;

            FileStream fileSteam = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileSteam);

            return binaryReader.ReadBytes((int)fileSteam.Length);
        }


        // Convert Byte array to Image source
        private ImageSource ConvertByteArrayToImageSource()
        {
            BitmapImage bitmapImage = new BitmapImage();
            MemoryStream memoryStream = new MemoryStream(ByteArray);

            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();

            return bitmapImage as ImageSource;
        }

    }
}
