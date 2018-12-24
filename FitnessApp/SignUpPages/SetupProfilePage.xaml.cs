using FitnessApp.Models;
using FitnessApp.SQLserver;
using FitnessApp.Windows;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FitnessApp.SignUpPages
{
    /// <summary>
    /// Interaction logic for SetUpProfilePage.xaml
    /// </summary>
    public partial class SetUpProfilePage : Page
    {
        private ImageModel profilePhoto = new ImageModel();

        public SetUpProfilePage()
        {
            InitializeComponent();
            SigningWindow.SetUpProfilePageObject = this;
        }

        private void SetUpProfileButton_Click(object sender, RoutedEventArgs e)
        {
            // Constarins to make sure that all fields are filled
            if (string.IsNullOrWhiteSpace(HeightTextBox            .Text) ||
                string.IsNullOrWhiteSpace(WeightTextBox            .Text) ||
                string.IsNullOrWhiteSpace(TargetWeightTextBox      .Text) ||
                string.IsNullOrWhiteSpace(KilosToLosePerWeekTextBox.Text) ||
                string.IsNullOrWhiteSpace(WorkoutsPerWeekTextBox   .Text) ||
                string.IsNullOrWhiteSpace(WorkoutHoursPerDayTextBox.Text))
            {
                SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("All fields are required!");
            }

            else
            {
                // Signing up
                Database.AddUser(profilePhoto.ByteArray,
                                 SigningWindow.SignUpPageObject.FirstNameTextBox.Text,
                                 SigningWindow.SignUpPageObject.LastNameTextBox.Text,
                                 SigningWindow.SignUpPageObject.EmailTextBox.Text,
                                 SigningWindow.SignUpPageObject.Password,
                                 SigningWindow.SignUpPageObject.GenderComboBox.Text,
                                 SigningWindow.SignUpPageObject.BirthDatePicker.Text,
                                 double.Parse(WeightTextBox.Text),
                                 double.Parse(HeightTextBox.Text),
                                 double.Parse(TargetWeightTextBox.Text),
                                 double.Parse(KilosToLosePerWeekTextBox.Text),
                                 double.Parse(WorkoutsPerWeekTextBox.Text),
                                 double.Parse(WorkoutHoursPerDayTextBox.Text));

                UserWindow UserWindowTemp = new UserWindow(Database.AccountID);
                SigningWindow.SigningWindowObject.Close();
                UserWindowTemp.Show();
            }
        }


        private void ChooseUserProfilePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog browsePhotoDialog = new OpenFileDialog();
            browsePhotoDialog.Title  = "Select your Profile Photo";
            browsePhotoDialog.Filter = "All formats|*.jpg;*.jpeg;*.png|" +
                                       "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                       "PNG (*.png)|*.png";


            if (browsePhotoDialog.ShowDialog() == true)
            {
                profilePhoto = new ImageModel(browsePhotoDialog.FileName);
                UserProfilePhoto.ImageSource = profilePhoto.Source;
            }
        }

        private void DecimalNumbersOnlyFieldValidation(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            var text = s.Text.Insert(s.SelectionStart, e.Text);
            e.Handled = !double.TryParse(text, out double d);
        }
    }
}
