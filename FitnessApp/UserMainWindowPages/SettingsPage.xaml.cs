using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {

        public SettingsPage()
        {
            InitializeComponent();
            UserMainWindow.SettingsPageObject = this;

            // Initialize Profile Expander to be expanded
            ProfileExpander.IsExpanded = true;

            // Initialize DataContext with signedInUser Model
            DataContext = UserMainWindow.signedInUser;
        }


        private void Expander_Expanded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Remove Expanded Event Handler from all Expanders
            // to prevent calling Expander_Expanded Event Handler 
            // recursively.
            ProfileExpander .Expanded -= Expander_Expanded;
            AccountExpander .Expanded -= Expander_Expanded;
            SecurityExpander.Expanded -= Expander_Expanded;
            AboutExpander   .Expanded -= Expander_Expanded;
            FeedbackExpander.Expanded -= Expander_Expanded;
            HelpExpander    .Expanded -= Expander_Expanded;

            // Close all Expanders.
            ProfileExpander .IsExpanded = false;
            AccountExpander .IsExpanded = false;
            SecurityExpander.IsExpanded = false;
            AboutExpander   .IsExpanded = false;
            FeedbackExpander.IsExpanded = false;
            HelpExpander    .IsExpanded = false;

            // Re-add Expanded Event Handler to all Expanders.
            ProfileExpander .Expanded += Expander_Expanded;
            AccountExpander .Expanded += Expander_Expanded;
            SecurityExpander.Expanded += Expander_Expanded;
            AboutExpander   .Expanded += Expander_Expanded;
            FeedbackExpander.Expanded += Expander_Expanded;
            HelpExpander    .Expanded += Expander_Expanded;


            // Get Current Expander object from sender.
            Expander currentExpander = sender as Expander;

            // Remove Expanded Event Handler from Current Expander.
            currentExpander.Expanded -= Expander_Expanded;

            // Open current Expander only.
            currentExpander.IsExpanded = true;

            // Re-add Expanded Event Handler to Current Expander.
            currentExpander.Expanded += Expander_Expanded;

        }


        private void UpdateUserProfilePhotoButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog browsePhotoDialog = new OpenFileDialog();
            browsePhotoDialog.Title  = "Select your New Profile Photo";
            browsePhotoDialog.Filter = "All formats|*.jpg;*.jpeg;*.png|" +
                                       "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                       "PNG (*.png)|*.png";

            if (browsePhotoDialog.ShowDialog() == true)
            {
                UserProfilePhoto.ImageSource = new BitmapImage(new Uri(browsePhotoDialog.FileName));
            }

        }
        
        private void TextBoxesNumbersOnly_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void UpdateProfileButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Update Profile Code Here...
        }

        private void UpdateAccountButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Update Account Code Here...
        }

        private void UpdatePasswordButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Update Password Code Here...
        }

        private void SubmitFeedbackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Submit Feedback Code Here...
        }

    }
}
