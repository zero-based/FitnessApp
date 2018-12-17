using FitnessApp.SQLdatabase;
using FitnessApp.Windows;
using System.Windows.Controls;

namespace FitnessApp.AdminWindowPages
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
            AdminWindow.AdminSettingsPageObject = this;

            // Initialize Profile Expander to be expanded
            AccountExpander.IsExpanded = true;

            // Initialize DataContext with signedInAdmin Model
            DataContext = AdminWindow.signedInAdmin;
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
                    AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("First Name Is Empty!");
                if (LastNameTextBox.Text == "")
                    AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Last Name Is Empty!");
                if (EmailTextBox.Text == "")
                    AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Email Is Empty!");
            }

            // Check Email Validation
            else if (!EmailTextBox.Text.Contains("@") || !EmailTextBox.Text.Contains(".com"))
                AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Invalid E-mail");

            // Check Email/Username Availability
            else if (EmailTextBox.Text != AdminWindow.signedInAdmin.Email)
            {
                if (SQLqueriesObject.IsEmailTaken(EmailTextBox.Text))
                    AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("E-mail is in use");
            }
            else
            {

                // Update signedInUser User Model
                AdminWindow.signedInAdmin.FirstName = FirstNameTextBox.Text;
                AdminWindow.signedInAdmin.LastName  = LastNameTextBox.Text;
                AdminWindow.signedInAdmin.Email     = EmailTextBox.Text;

                // Update User's Account in database
                SQLqueriesObject.UpdateAdminAccount(AdminWindow.signedInAdmin);

                // Refresh UserWindow DataContext
                AdminWindow.AdminWindowObject.DataContext = null;
                AdminWindow.AdminWindowObject.DataContext = AdminWindow.signedInAdmin;

                // Confirmation Message
                AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Account Updated!");

            }
        }

        private void UpdatePasswordButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (SQLqueriesObject.EncryptPassword(OldPasswordTextBox.Password) != AdminWindow.signedInAdmin.Password)
            {
                AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Old Password is Incorrect!");
                OldPasswordTextBox.Password = "";
            }
            else if (NewPasswordTextBox.Password != ConfirmNewPasswordTextBox.Password)
            {
                AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("New Password and Confirmation Mismatch!");
                NewPasswordTextBox.Password = "";
                ConfirmNewPasswordTextBox.Password = "";
            }
            else if (NewPasswordTextBox.Password.Length < 7)
            {
                AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Password must be more than 7 Charachters!");
                NewPasswordTextBox.Password = "";
                ConfirmNewPasswordTextBox.Password = "";
            }
            else
            {
                // Update signedInAdmin User Model
                AdminWindow.signedInAdmin.Password = SQLqueriesObject.EncryptPassword(NewPasswordTextBox.Password);

                // Update Admin's Password in database
                SQLqueriesObject.UpdateAdminPassword(AdminWindow.signedInAdmin);

                // Confirmation Message
                AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Password Updated!");
            }
        }
    }
}
