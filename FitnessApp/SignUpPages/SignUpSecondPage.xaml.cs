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
        public static SignUpSecondPage SignUpSecondPageObject = new SignUpSecondPage();

        public SignUpSecondPage()
        {
            InitializeComponent();
            SignUpSecondPageObject = this;
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
                NavigationService.Navigate(SetUpProfilePage.SetUpProfilePageObject);

                //Change Back Card Header
                SigningWindow.SigningWindowObject.BackArrowButton.Visibility = Visibility.Hidden;
                SigningWindow.SigningWindowObject.BackCardHeaderTextBlock.Text = "Set up your Profile";
                SigningWindow.SigningWindowObject.BackCardHeaderTextBlock.Margin = new Thickness(-15);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpFirstPage.SignUpFirstPageObject.PasswordTextBox.Password = SignUpFirstPage.SignUpFirstPageObject.Password;
            SignUpFirstPage.SignUpFirstPageObject.ConfirmPasswordTextBox.Password = SignUpFirstPage.SignUpFirstPageObject.ConfirmedPassword;

            NavigationService.Navigate(SignUpFirstPage.SignUpFirstPageObject);
        }
    }
}
