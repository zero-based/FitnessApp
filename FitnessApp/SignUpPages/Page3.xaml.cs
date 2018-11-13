using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
namespace FitnessApp.SignUpPages
{
    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public static Page3 PageThreeObject = new Page3();

        public Page3()
        {
            InitializeComponent();
            PageThreeObject = this;
        }

        private void SetUpProfile_Click(object sender, RoutedEventArgs e)
        {
            UserMainWindow AppWindow = new UserMainWindow();
            SigningWindow.SigningWindowObject.Close();
            AppWindow.ShowDialog();
        }

        private void ChooseImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog BrowseImageDialogBox = new OpenFileDialog();
            BrowseImageDialogBox.Title = "Select a picture";
            BrowseImageDialogBox.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (BrowseImageDialogBox.ShowDialog() == true)
            {
                UserProfilePhoto.ImageSource = new BitmapImage(new Uri(BrowseImageDialogBox.FileName));
                UserProfilePhoto.Opacity = 1.0;
            }
        }
    }
}
