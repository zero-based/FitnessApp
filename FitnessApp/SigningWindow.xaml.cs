using FitnessApp.SQLdatabase;
using MaterialDesignThemes.Wpf;
using System.Windows;

namespace FitnessApp
{
    /// <summary>
    /// Interaction logic for SigningWindow.xaml
    /// </summary>
    public partial class SigningWindow : Window
    {
        public static SigningWindow SigningWindowObject;

        // Create object from database class
        SQLqueries SQLqueriesObject = new SQLqueries();

        public SigningWindow()
        {
            InitializeComponent();
            SigningWindowObject = this;

            // Intialize ErrorMessagesQueue and Assign it to ErrorsSnackbar's MessageQueue
            var ErrorMessagesQueue = new SnackbarMessageQueue(System.TimeSpan.FromMilliseconds(2000));
            ErrorsSnackbar.MessageQueue = ErrorMessagesQueue;
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {

            bool isAccountFound = SQLqueriesObject.SignIn(EmailSignInTextBox.Text, PasswordSignInTextBox.Password);

            if (isAccountFound == true)
            {
                if (SQLqueriesObject.accountType == "User")
                {
                    // Open User Main Window
                    UserMainWindow UserMainWindowTemp = new UserMainWindow();
                    UserMainWindowTemp.Show();
                }
                else
                {
                    // Open Admin Main Window
                    AdminMainWindow AdminMainWindowTemp = new AdminMainWindow();
                    AdminMainWindowTemp.Show();
                }

                // Close Signing Window
                Close();
            }
            else
            {
                // Display error when the user is not found
                ErrorsSnackbar.MessageQueue.Enqueue("Incorrect Email Or Password");
            }

        }


        private void CreateAnAccountButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpPagesFrame.NavigationService.Navigate(SignUpPages.SignUpFirstPage.SignUpFirstPageObject);
        }
    }
}
