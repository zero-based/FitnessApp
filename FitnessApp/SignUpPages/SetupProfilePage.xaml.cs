using FitnessApp.SQLdatabase;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows;
using FitnessApp.Models;
using System.Windows.Input;

namespace FitnessApp.SignUpPages
{
    /// <summary>
    /// Interaction logic for SetUpProfilePage.xaml
    /// </summary>
    public partial class SetUpProfilePage : Page
    {
        public static SetUpProfilePage SetUpProfilePageObject = new SetUpProfilePage();
        private ImageModel profilePhoto = new ImageModel();

        // Create an object from dataBase class
        SQLqueries SQLqueriesObject = new SQLqueries();

        public SetUpProfilePage()
        {
            InitializeComponent();
            SetUpProfilePageObject = this;
        }

        private void SetUpProfileButton_Click(object sender, RoutedEventArgs e)
        {
            int heightLength = HeightTextBox.Text.Length;
            int weightLength = WeightTextBox.Text.Length;
            int targetWeightLength = TargetWeightTextBox.Text.Length;
            int kilosToLosePerWeekLength = KilosToLosePerWeekTextBox.Text.Length;
            int workoutsPerWeekLength = WorkoutsPerWeekTextBox.Text.Length;
            int workoutHoursPerDayLength = WorkoutHoursPerDayTextBox.Text.Length;

            // Constarins to make sure that all fields are filled with numbers
            if (heightLength < 1 || weightLength < 1 || targetWeightLength < 1 || kilosToLosePerWeekLength < 1 || workoutsPerWeekLength < 1 || workoutHoursPerDayLength < 1)
            {
                SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("All fields are required!");
            }

            else
            {
                // Signing up
                SQLqueriesObject.SignUp(profilePhoto.ByteArray,
                                        SignUpFirstPage.SignUpFirstPageObject.FirstNameTextBox.Text,
                                        SignUpFirstPage.SignUpFirstPageObject.LastNameTextBox.Text,
                                        SignUpFirstPage.SignUpFirstPageObject.UsernameTextBox.Text,
                                        SignUpFirstPage.SignUpFirstPageObject.EmailTextBox.Text,
                                        SignUpFirstPage.SignUpFirstPageObject.Password,
                                        SignUpSecondPage.SignUpSecondPageObject.GenderComboBox.Text,
                                        SignUpSecondPage.SignUpSecondPageObject.BirthDatePicker.Text,
                                        double.Parse(WeightTextBox.Text),
                                        double.Parse(HeightTextBox.Text),
                                        double.Parse(TargetWeightTextBox.Text),
                                        double.Parse(KilosToLosePerWeekTextBox.Text),
                                        double.Parse(WorkoutsPerWeekTextBox.Text),
                                        double.Parse(WorkoutHoursPerDayTextBox.Text));

                UserMainWindow UserMainWindowTemp = new UserMainWindow(SQLqueriesObject.accountID);
                SigningWindow.SigningWindowObject.Close();
                UserMainWindowTemp.Show();
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
