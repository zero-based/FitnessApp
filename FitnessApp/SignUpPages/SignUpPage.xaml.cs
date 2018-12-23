using FitnessApp.SQLserver;
using FitnessApp.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FitnessApp.SignUpPages
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {

        public static SignUpPage SignUpPageObject = new SignUpPage();

        public SignUpPage()
        {
            InitializeComponent();
            SignUpPageObject = this;
        }



        private string password;
        private string confirmedPassword;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string ConfirmedPassword
        {
            get { return confirmedPassword; }
            set { confirmedPassword = value; }
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            // Save entered password and its confirmation
            password = PasswordTextBox.Password;
            confirmedPassword = ConfirmPasswordTextBox.Password;

            // Constraints , to make sure that texts boxes are not empty
            if (FirstNameTextBox.Text.Length < 1 || LastNameTextBox.Text.Length < 1 || PasswordTextBox.Password.Length < 1 ||
               GenderComboBox.Text.Length < 4 || BirthDatePicker.Text.Length < 5 || (NotRobotCheckBox.IsChecked == false))
            {
                SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("All fields are required!");
            }
            else if (!EmailTextBox.Text.Contains("@") || !EmailTextBox.Text.Contains(".com"))
            {
                SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("Invalid E-mail");
            }
            else if (Database.IsEmailTaken(EmailTextBox.Text))
            {
                SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("Email is already taken!");
            }
            else if (PasswordTextBox.Password.Length < 6)
            {
                SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("password < 7!");
            }
            else if (PasswordTextBox.Password != ConfirmPasswordTextBox.Password)
            {
                SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("Password doesn't match confirmation!");
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
    }
}
