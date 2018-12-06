using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace FitnessApp.SignUpPages
{
    /// <summary>
    /// Interaction logic for SetUpProfilePage.xaml
    /// </summary>
    public partial class SetUpProfilePage : Page
    {
        public static SetUpProfilePage SetUpProfilePageObject = new SetUpProfilePage();

        public SetUpProfilePage()
        {
            InitializeComponent();
            SetUpProfilePageObject = this;
        }

        private void SetUpProfileButton_Click(object sender, RoutedEventArgs e)
        {
            UserMainWindow UserMainWindowTemp = new UserMainWindow();
            SigningWindow.SigningWindowObject.Close();
            UserMainWindowTemp.ShowDialog();
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
