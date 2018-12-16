using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;

namespace FitnessApp.SignUpPages
{
    /// <summary>
    /// Interaction logic for SignUpSecondPage.xaml
    /// </summary>
    public partial class SignUpSecondPage : Page
    {
        public SignUpSecondPage()
        {
            InitializeComponent();
            SigningWindow.SignUpSecondPageObject = this;
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            // The length of each Box
            int genderIsSelected = GenderComboBox.Text.Length;
            int calenderIsSelected = BirthDatePicker.Text.Length;

            // Making sure that user filled all the requirments 
            if (genderIsSelected < 4 || calenderIsSelected < 5 || (NotRobotCheckBox.IsChecked == false))
            {
                SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("All fields are required!");
            }

            else
            {
                NavigationService.Navigate(SigningWindow.SetUpProfilePageObject);

                //Change Back Card Header
                SigningWindow.SigningWindowObject.BackArrowButton.Visibility = Visibility.Hidden;
                SigningWindow.SigningWindowObject.BackCardHeaderTextBlock.Text = "Set up your Profile";
                SigningWindow.SigningWindowObject.BackCardHeaderTextBlock.Margin = new Thickness(-15);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SigningWindow.SignUpFirstPageObject.PasswordTextBox.Password = SigningWindow.SignUpFirstPageObject.Password;
            SigningWindow.SignUpFirstPageObject.ConfirmPasswordTextBox.Password = SigningWindow.SignUpFirstPageObject.ConfirmedPassword;

            NavigationService.Navigate(SigningWindow.SignUpFirstPageObject);
        }
    }
}
