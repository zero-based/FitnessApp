using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Controls;
using FitnessApp.Models;
using FitnessApp.SQLdatabase;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        // Create an object from dataBase class
        SQLqueries SQLqueriesObject = new SQLqueries();

        public SettingsPage()
        {
            InitializeComponent();
            UserMainWindow.SettingsPageObject = this;

            // Initialize Profile Expander to be expanded
            ProfileExpander.IsExpanded = true;

            // Initialize DataContext with signedInUser Model
            DataContext = UserMainWindow.signedInUser;
        }


        private void Expander_Expanded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Remove Expanded Event Handler from all Expanders
            // to prevent calling Expander_Expanded Event Handler 
            // recursively.
            ProfileExpander .Expanded -= Expander_Expanded;
            AccountExpander .Expanded -= Expander_Expanded;
            SecurityExpander.Expanded -= Expander_Expanded;
            AboutExpander   .Expanded -= Expander_Expanded;
            FeedbackExpander.Expanded -= Expander_Expanded;
            HelpExpander    .Expanded -= Expander_Expanded;

            // Close all Expanders.
            ProfileExpander .IsExpanded = false;
            AccountExpander .IsExpanded = false;
            SecurityExpander.IsExpanded = false;
            AboutExpander   .IsExpanded = false;
            FeedbackExpander.IsExpanded = false;
            HelpExpander    .IsExpanded = false;

            // Re-add Expanded Event Handler to all Expanders.
            ProfileExpander .Expanded += Expander_Expanded;
            AccountExpander .Expanded += Expander_Expanded;
            SecurityExpander.Expanded += Expander_Expanded;
            AboutExpander   .Expanded += Expander_Expanded;
            FeedbackExpander.Expanded += Expander_Expanded;
            HelpExpander    .Expanded += Expander_Expanded;


            // Get Current Expander object from sender.
            Expander currentExpander = sender as Expander;

            // Remove Expanded Event Handler from Current Expander.
            currentExpander.Expanded -= Expander_Expanded;

            // Open current Expander only.
            currentExpander.IsExpanded = true;

            // Re-add Expanded Event Handler to Current Expander.
            currentExpander.Expanded += Expander_Expanded;

        }

        private ImageModel currentProfilePhoto;

        private void UpdateUserProfilePhotoButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog browsePhotoDialog = new OpenFileDialog();
            browsePhotoDialog.Title  = "Select your New Profile Photo";
            browsePhotoDialog.Filter = "All formats|*.jpg;*.jpeg;*.png|" +
                                       "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                       "PNG (*.png)|*.png";

            if (browsePhotoDialog.ShowDialog() == true)
            {
                currentProfilePhoto = new ImageModel(browsePhotoDialog.FileName);
                UserProfilePhoto.ImageSource = currentProfilePhoto.Source;
            }

        }
        
        private void DecimalNumbersOnlyFieldValidation(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            var text = s.Text.Insert(s.SelectionStart, e.Text);
            e.Handled = !double.TryParse(text, out double d);
        }

        private void UpdateProfileButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            if (WeightTextBox.Text == ""             || HeightTextBox.Text == ""          || TargetWeightTextBox.Text == ""     ||
                KilosToLosePerWeekTextBox.Text == "" || WorkoutsPerWeekTextBox.Text == "" || WorkoutHoursPerDayTextBox.Text == "")
            {
                if (WeightTextBox.Text == "")
                    UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Weight Is Empty!");
                if (HeightTextBox.Text == "")
                    UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Height Is Empty!");
                if (TargetWeightTextBox.Text == "")
                    UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Target Weight Is Empty!");
                if (KilosToLosePerWeekTextBox.Text == "")
                    UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Kilos To Lose Per Week Is Empty!");
                if (WorkoutsPerWeekTextBox.Text == "")
                    UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Workouts Per Week Is Empty!");
                if (WorkoutHoursPerDayTextBox.Text == "")
                    UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Workout Hours Per Day Is Empty!");
            }

            else
            {

                // Update signedInUser User Model

                if (currentProfilePhoto != null) // Check if profile photo is updated
                    UserMainWindow.signedInUser.ProfilePhoto   = currentProfilePhoto;

                UserMainWindow.signedInUser.Weight             = double.Parse(WeightTextBox            .Text);
                UserMainWindow.signedInUser.Height             = double.Parse(HeightTextBox            .Text);
                UserMainWindow.signedInUser.TargetWeight       = double.Parse(TargetWeightTextBox      .Text);
                UserMainWindow.signedInUser.KilosToLosePerWeek = double.Parse(KilosToLosePerWeekTextBox.Text);
                UserMainWindow.signedInUser.WorkoutsPerWeek    = double.Parse(WorkoutsPerWeekTextBox   .Text);
                UserMainWindow.signedInUser.WorkoutHoursPerDay = double.Parse(WorkoutHoursPerDayTextBox.Text);

                // Update User's Profile in database
                SQLqueriesObject.UpdateUserProfile(UserMainWindow.signedInUser);

                // Refresh UserMainWindow DataContext
                UserMainWindow.UserMainWindowObject.DataContext = null;
                UserMainWindow.UserMainWindowObject.DataContext = UserMainWindow.signedInUser;

                // Refresh CaloriesCalculatorPage DataContext
                UserMainWindow.CaloriesCalculatorPageObject.DataContext = null;
                UserMainWindow.CaloriesCalculatorPageObject.DataContext = UserMainWindow.signedInUser;

                // Refresh Weight and Calories Cards in Home Page
                UserMainWindow.HomePageObject.WeightChart.DataContext = null;
                UserMainWindow.HomePageObject.LoadWeightChart();
                UserMainWindow.HomePageObject.LoadTotalWeightLostCard();
                UserMainWindow.HomePageObject.LoadAverageWeightLostCard();
                UserMainWindow.HomePageObject.LoadCaloriesCard();

                // Confirmation Message
                UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Profile Updated!");

            }
        }

        private void UpdateAccountButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (FirstNameTextBox.Text == "" || LastNameTextBox.Text == "" ||
                UsernameTextBox.Text == ""  || EmailTextBox.Text == ""     )
            {
                if (FirstNameTextBox.Text == "")
                    UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("First Name Is Empty!");
                if (LastNameTextBox.Text == "")
                    UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Last Name Is Empty!");
                if (UsernameTextBox.Text == "")
                    UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Username Is Empty!");
                if (EmailTextBox.Text == "")
                    UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Email Is Empty!");
            }

            // Check Email Validation
            else if (!EmailTextBox.Text.Contains("@") || !EmailTextBox.Text.Contains(".com"))
                UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Invalid E-mail");

            // Check Email/Username Availability
            else if (SQLqueriesObject.IsEmailTaken(EmailTextBox.Text) || EmailTextBox.Text != UserMainWindow.signedInUser.Email)
                UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("E-mail is in use");

            else if (SQLqueriesObject.IsUsernameTaken(UsernameTextBox.Text) || UsernameTextBox.Text != UserMainWindow.signedInUser.Username)
                UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Username is in use");

            else
            {

                // Update signedInUser User Model
                UserMainWindow.signedInUser.FirstName = FirstNameTextBox.Text;
                UserMainWindow.signedInUser.LastName  = LastNameTextBox.Text;
                UserMainWindow.signedInUser.Username  = UsernameTextBox.Text;
                UserMainWindow.signedInUser.Email     = EmailTextBox.Text;

                // Update User's Account in database
                SQLqueriesObject.UpdateUserAccount(UserMainWindow.signedInUser);

                // Refresh UserMainWindow DataContext
                UserMainWindow.UserMainWindowObject.DataContext = null;
                UserMainWindow.UserMainWindowObject.DataContext = UserMainWindow.signedInUser;

                // Confirmation Message
                UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Account Updated!");

            }
        }

        private void UpdatePasswordButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (SQLqueriesObject.EncryptPassword(OldPasswordTextBox.Password) != UserMainWindow.signedInUser.Password)
            {
                UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Old Password is Incorrect!");
                OldPasswordTextBox.Password = "";
            }

            else if (NewPasswordTextBox.Password != ConfirmNewPasswordTextBox.Password)
            {
                UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("New Password and Confirmation Mismatch!");
                NewPasswordTextBox.Password = "";
                ConfirmNewPasswordTextBox.Password = "";
            }

            else
            {

                // Update signedInUser User Model
                UserMainWindow.signedInUser.Password = SQLqueriesObject.EncryptPassword(NewPasswordTextBox.Password);

                // Update User's Password in database
                SQLqueriesObject.UpdateUserPassword(UserMainWindow.signedInUser);

                // Confirmation Message
                UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Password Updated!");
            }
        }

        private void SubmitFeedbackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SQLqueriesObject.SaveFeedback(UserMainWindow.signedInUser.ID, RatingBar.Value , FeedbackTextBox.Text);

            // Confirmation Message
            UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Thank you for your feedback!");

        }

    }
}
