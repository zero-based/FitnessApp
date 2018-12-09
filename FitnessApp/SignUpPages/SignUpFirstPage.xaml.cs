using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using FitnessApp.SQLdatabase;
namespace FitnessApp.SignUpPages
{
    /// <summary>
    /// Interaction logic for SignUpFirstPage.xaml
    /// </summary>
    public partial class SignUpFirstPage : Page
    {
        // creating an object from the sql class
        SQLqueries objectSqlQueries = new SQLqueries();

        public static SignUpFirstPage SignUpFirstPageObject = new SignUpFirstPage();

        public SignUpFirstPage()
        {
            InitializeComponent();
            SignUpFirstPageObject = this;
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

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // Save entered password and its confirmation
            password = PasswordTextBox.Password;
            confirmedPassword = ConfirmPasswordTextBox.Password;

            // Get the length of each input
            int passwordLength = PasswordTextBox.Password.Length;
            int firstNameLength = FirstNameTextBox.Text.Length;
            int lastNameLength = LastNameTextBox.Text.Length;
            int userNameLength = UsernameTextBox.Text.Length;
            bool CheckUniqueUserName = objectSqlQueries.IsUsernameTaken(UsernameTextBox.Text);
            bool CheckUniqueEmail = objectSqlQueries.IsEmailTaken(EmailTextBox.Text);
            // Constraints , to make sure that texts boxes are not empty
            if (firstNameLength < 1 || lastNameLength < 1 || userNameLength < 1 || passwordLength < 1)
            {

                SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("All fields are required!");
            }
            else
            {
                if (PasswordTextBox.Password == ConfirmPasswordTextBox.Password && passwordLength > 6 && CheckUniqueUserName == false && CheckUniqueEmail == false)
                {
                    if (EmailTextBox.Text.Contains("@"))
                    {
                        if (EmailTextBox.Text.Contains(".com"))
                        {

                            NavigationService.Navigate(SignUpSecondPage.SignUpSecondPageObject);
                        }
                    }
                    else
                    {
                        SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("Invalid E-mail");
                    }
                }
                else
                {
                    SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("password != confirmation password OR password < 7 ");
                }


                if (CheckUniqueUserName == true && CheckUniqueEmail == false)
                {
                    SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("User Name Is Already Taken ");

                }

                if (CheckUniqueEmail == true && CheckUniqueUserName == false)
                {
                    SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue(" Email Is Already Taken");
                }
                if (CheckUniqueEmail == true && CheckUniqueUserName == true)
                {
                    SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue(" Email and user name are already taken");
                }

            }
        }
    }
}
