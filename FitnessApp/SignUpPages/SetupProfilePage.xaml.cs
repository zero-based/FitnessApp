using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using FitnessApp.SQLdatabase;
using System.IO;
using System.Text.RegularExpressions;


namespace FitnessApp.SignUpPages
{
    /// <summary>
    /// Interaction logic for SetUpProfilePage.xaml
    /// </summary>
    public partial class SetUpProfilePage : Page
    {
        public static SetUpProfilePage SetUpProfilePageObject = new SetUpProfilePage();

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
                SQLqueriesObject.SignUp(ref profilePhotoByteArray,
                                        SignUpFirstPage.SignUpFirstPageObject.FirstNameTextBox.Text,
                                        SignUpFirstPage.SignUpFirstPageObject.LastNameTextBox.Text,
                                        SignUpFirstPage.SignUpFirstPageObject.UsernameTextBox.Text,
                                        SignUpFirstPage.SignUpFirstPageObject.EmailTextBox.Text,
                                        SignUpFirstPage.SignUpFirstPageObject.Password,
                                        SignUpSecondPage.SignUpSecondPageObject.GenderComboBox.Text,
                                        SignUpSecondPage.SignUpSecondPageObject.BirthDatePicker.Text,
                                        double.Parse(WeightTextBox.Text), double.Parse(HeightTextBox.Text),
                                        double.Parse(TargetWeightTextBox.Text),
                                        double.Parse(KilosToLosePerWeekTextBox.Text),
                                        int.Parse(WorkoutsPerWeekTextBox.Text),
                                        int.Parse(WorkoutHoursPerDayTextBox.Text));

                UserMainWindow UserMainWindowTemp = new UserMainWindow();
                SigningWindow.SigningWindowObject.Close();
                UserMainWindowTemp.Show();
            }
        }

        public byte[] profilePhotoByteArray = null;
        private void ChooseUserProfilePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog browsePhotoDialog = new OpenFileDialog();
            browsePhotoDialog.Title  = "Select your Profile Photo";
            browsePhotoDialog.Filter = "All formats|*.jpg;*.jpeg;*.png|" +
                                       "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                       "PNG (*.png)|*.png";

            if (browsePhotoDialog.ShowDialog() == true)
            {
                UserProfilePhoto.ImageSource = new BitmapImage(new Uri(browsePhotoDialog.FileName));
                UserProfilePhoto.Opacity = 1.0;

                // Convert the image to byte[]
                FileStream fs = new FileStream(browsePhotoDialog.FileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                profilePhotoByteArray = br.ReadBytes((int)fs.Length);
            }
        }

        private void TextBoxesNumbersOnly_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
