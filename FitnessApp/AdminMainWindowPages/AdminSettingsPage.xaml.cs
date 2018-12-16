using FitnessApp.SQLdatabase;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace FitnessApp.AdminMainWindowPages
{
    /// <summary>
    /// Interaction logic for AdminSettingsPage.xaml
    /// </summary>
    public partial class AdminSettingsPage : Page
    {
        SQLqueries SQLqueriesObject = new SQLqueries();

        public AdminSettingsPage()
        {
            InitializeComponent();
            AdminMainWindow.AdminSettingsPageObject = this;

            // Initialize Profile Expander to be expanded
            AccountExpander.IsExpanded = true;

            // Initialize DataContext with signedInAdmin Model
            DataContext = AdminMainWindow.signedInAdmin;
        }

        private void Expander_Expanded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Remove Expanded Event Handler from all Expanders
            // to prevent calling Expander_Expanded Event Handler 
            // recursively.
            AccountExpander.Expanded  -= Expander_Expanded;
            SecurityExpander.Expanded -= Expander_Expanded;
            HelpExpander.Expanded     -= Expander_Expanded;

            // Close all Expanders.
            AccountExpander.IsExpanded  = false;
            SecurityExpander.IsExpanded = false;
            HelpExpander.IsExpanded     = false;

            // Re-add Expanded Event Handler to all Expanders.
            AccountExpander.Expanded  += Expander_Expanded;
            SecurityExpander.Expanded += Expander_Expanded;
            HelpExpander.Expanded     += Expander_Expanded;


            // Get Current Expander object from sender.
            Expander currentExpander = sender as Expander;

            // Remove Expanded Event Handler from Current Expander.
            currentExpander.Expanded -= Expander_Expanded;

            // Open current Expander only.
            currentExpander.IsExpanded = true;

            // Re-add Expanded Event Handler to Current Expander.
            currentExpander.Expanded += Expander_Expanded;

        }


        private void UpdateAccountButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (FirstNameTextBox.Text == "" || LastNameTextBox.Text == "" || EmailTextBox.Text == "")
            {
                if (FirstNameTextBox.Text == "")
                    AdminMainWindow.AdminMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("First Name Is Empty!");
                if (LastNameTextBox.Text == "")
                    AdminMainWindow.AdminMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Last Name Is Empty!");
                if (EmailTextBox.Text == "")
                    AdminMainWindow.AdminMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Email Is Empty!");
            }

            // Check Email Validation
            else if (!EmailTextBox.Text.Contains("@") || !EmailTextBox.Text.Contains(".com"))
                AdminMainWindow.AdminMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Invalid E-mail");

            // Check Email/Username Availability
            else if (EmailTextBox.Text != AdminMainWindow.signedInAdmin.Email)
            {
                if (SQLqueriesObject.IsEmailTaken(EmailTextBox.Text))
                    AdminMainWindow.AdminMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("E-mail is in use");
            }
            else
            {

                // Update signedInUser User Model
                AdminMainWindow.signedInAdmin.FirstName = FirstNameTextBox.Text;
                AdminMainWindow.signedInAdmin.LastName  = LastNameTextBox.Text;
                AdminMainWindow.signedInAdmin.Email     = EmailTextBox.Text;

                // Update User's Account in database
                SQLqueriesObject.UpdateAdminAccount(AdminMainWindow.signedInAdmin);

                // Refresh UserMainWindow DataContext
                AdminMainWindow.AdminMainWindowObject.DataContext = null;
                AdminMainWindow.AdminMainWindowObject.DataContext = AdminMainWindow.signedInAdmin;

                // Confirmation Message
                AdminMainWindow.AdminMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Account Updated!");

            }
        }

        private void UpdatePasswordButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (SQLqueriesObject.EncryptPassword(OldPasswordTextBox.Password) != AdminMainWindow.signedInAdmin.Password)
            {
                AdminMainWindow.AdminMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Old Password is Incorrect!");
                OldPasswordTextBox.Password = "";
            }
            else if (NewPasswordTextBox.Password != ConfirmNewPasswordTextBox.Password)
            {
                AdminMainWindow.AdminMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("New Password and Confirmation Mismatch!");
                NewPasswordTextBox.Password = "";
                ConfirmNewPasswordTextBox.Password = "";
            }
            else if (NewPasswordTextBox.Password.Length < 7)
            {
                AdminMainWindow.AdminMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Password must be more than 7 Charachters!");
                NewPasswordTextBox.Password = "";
                ConfirmNewPasswordTextBox.Password = "";
            }
            else
            {
                // Update signedInAdmin User Model
                AdminMainWindow.signedInAdmin.Password = SQLqueriesObject.EncryptPassword(NewPasswordTextBox.Password);

                // Update Admin's Password in database
                SQLqueriesObject.UpdateAdminPassword(AdminMainWindow.signedInAdmin);

                // Confirmation Message
                AdminMainWindow.AdminMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Password Updated!");
            }
        }
    }
}
