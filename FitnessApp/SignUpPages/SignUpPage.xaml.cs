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

        public SignUpPage()
        {
            InitializeComponent();
            SigningWindow.SignUpPageObject = this;
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
            Password = PasswordTextBox.Password;
            ConfirmedPassword = ConfirmPasswordTextBox.Password;

            // Empty Fields Validation
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text)    ||
                string.IsNullOrWhiteSpace(LastNameTextBox .Text)    ||
                string.IsNullOrWhiteSpace(PasswordTextBox.Password) ||
                GenderComboBox.SelectedIndex == -1                  ||
                BirthDatePicker.SelectedDate == null                ||
                NotRobotCheckBox.IsChecked   == false)
            {
                SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("All fields are required!");
            }

            else if (!EmailTextBox.Text.Contains("@") || !EmailTextBox.Text.Contains(".com"))
                SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("Invalid E-mail");

            else if (Database.IsEmailTaken(EmailTextBox.Text))
                SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("Email is already taken!");

            else if (PasswordTextBox.Password != ConfirmPasswordTextBox.Password)
                SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("Password and Confirmation doesn't match!");

            else if (PasswordTextBox.Password.Length < 7)
                SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("Password must be 7 characters or more");

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
