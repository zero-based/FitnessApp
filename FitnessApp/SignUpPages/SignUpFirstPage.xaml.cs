using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FitnessApp.SignUpPages
{
    /// <summary>
    /// Interaction logic for SignUpFirstPage.xaml
    /// </summary>
    public partial class SignUpFirstPage : Page
    {
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

            NavigationService.Navigate(SignUpSecondPage.SignUpSecondPageObject);
        }
    }
}
