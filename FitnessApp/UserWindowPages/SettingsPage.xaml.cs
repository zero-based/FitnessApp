using FitnessApp.Models;
using FitnessApp.SQLdatabase;
using FitnessApp.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Input;

namespace FitnessApp.UserWindowPages
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
            UserWindow.SettingsPageObject = this;

            // Initialize Profile Expander to be expanded
            ProfileExpander.IsExpanded = true;

            // Initialize DataContext with signedInUser Model
            DataContext = UserWindow.signedInUser;
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
                    UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Weight Is Empty!");
                if (HeightTextBox.Text == "")
                    UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Height Is Empty!");
                if (TargetWeightTextBox.Text == "")
                    UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Target Weight Is Empty!");
                if (KilosToLosePerWeekTextBox.Text == "")
                    UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Kilos To Lose Per Week Is Empty!");
                if (WorkoutsPerWeekTextBox.Text == "")
                    UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Workouts Per Week Is Empty!");
                if (WorkoutHoursPerDayTextBox.Text == "")
                    UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Workout Hours Per Day Is Empty!");
            }

            else
            {

                // Update signedInUser User Model

                if (currentProfilePhoto != null) // Check if profile photo is updated
                    UserWindow.signedInUser.ProfilePhoto   = currentProfilePhoto;

                UserWindow.signedInUser.Weight             = double.Parse(WeightTextBox            .Text);
                UserWindow.signedInUser.Height             = double.Parse(HeightTextBox            .Text);
                UserWindow.signedInUser.TargetWeight       = double.Parse(TargetWeightTextBox      .Text);
                UserWindow.signedInUser.KilosToLosePerWeek = double.Parse(KilosToLosePerWeekTextBox.Text);
                UserWindow.signedInUser.WorkoutsPerWeek    = double.Parse(WorkoutsPerWeekTextBox   .Text);
                UserWindow.signedInUser.WorkoutHoursPerDay = double.Parse(WorkoutHoursPerDayTextBox.Text);

                // Update User's Profile in database
                SQLqueriesObject.UpdateUserProfile(UserWindow.signedInUser);

                // Refresh UserWindow DataContext
                UserWindow.UserWindowObject.DataContext = null;
                UserWindow.UserWindowObject.DataContext = UserWindow.signedInUser;

                // Refresh CaloriesCalculatorPage DataContext
                UserWindow.CaloriesCalculatorPageObject.DataContext = null;
                UserWindow.CaloriesCalculatorPageObject.DataContext = UserWindow.signedInUser;

                // Refresh Weight and Calories Cards in Home Page
                UserWindow.HomePageObject.WeightChart.DataContext = null;
                UserWindow.HomePageObject.LoadWeightChart();
                UserWindow.HomePageObject.LoadTotalWeightLostCard();
                UserWindow.HomePageObject.LoadAverageWeightLostCard();
                UserWindow.HomePageObject.LoadCaloriesCard();

                // Confirmation Message
                UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Profile Updated!");

            }
        }

        private void UpdateAccountButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (FirstNameTextBox.Text == "" || LastNameTextBox.Text == "" ||
                UsernameTextBox.Text == "" || EmailTextBox.Text == "")
            {
                if (FirstNameTextBox.Text == "")
                    UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("First Name is Empty!");
                if (LastNameTextBox.Text == "")
                    UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Last Name is Empty!");
                if (UsernameTextBox.Text == "")
                    UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Username is Empty!");
                if (EmailTextBox.Text == "")
                    UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Email is Empty!");
            }

            // Check Email Validation
            else if (!EmailTextBox.Text.Contains("@") || !EmailTextBox.Text.Contains(".com"))
                UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Invalid E-mail");

            // Check Email/Username Availability
            else if (EmailTextBox.Text != UserWindow.signedInUser.Email)
            {
                if (SQLqueriesObject.IsEmailTaken(EmailTextBox.Text))
                    UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("E-mail is in use");
            }

            else if (UsernameTextBox.Text != UserWindow.signedInUser.Username)
            {
                if (SQLqueriesObject.IsUsernameTaken(UsernameTextBox.Text))
                    UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Username is in use");
            }

            else
            {

                // Update signedInUser User Model
                UserWindow.signedInUser.FirstName = FirstNameTextBox.Text;
                UserWindow.signedInUser.LastName = LastNameTextBox.Text;
                UserWindow.signedInUser.Username = UsernameTextBox.Text;
                UserWindow.signedInUser.Email = EmailTextBox.Text;

                // Update User's Account in database
                SQLqueriesObject.UpdateUserAccount(UserWindow.signedInUser);

                // Refresh UserWindow DataContext
                UserWindow.UserWindowObject.DataContext = null;
                UserWindow.UserWindowObject.DataContext = UserWindow.signedInUser;

                // Confirmation Message
                UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Account Updated!");

            }
        }

        private void UpdatePasswordButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (OldPasswordTextBox.Password == "" || NewPasswordTextBox.Password == "" || ConfirmNewPasswordTextBox.Password == "")
            {
                UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("All fields are required!");
            }

            else if (SQLqueriesObject.EncryptPassword(OldPasswordTextBox.Password) != UserWindow.signedInUser.Password)
            {
                UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Old Password is Incorrect!");
                OldPasswordTextBox.Password = "";
            }

            else if (NewPasswordTextBox.Password != ConfirmNewPasswordTextBox.Password)
            {
                UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("New Password and Confirmation Mismatch!");
                NewPasswordTextBox.Password = "";
                ConfirmNewPasswordTextBox.Password = "";
            }

            else
            {

                // Update signedInUser User Model
                UserWindow.signedInUser.Password = SQLqueriesObject.EncryptPassword(NewPasswordTextBox.Password);

                // Update User's Password in database
                SQLqueriesObject.UpdateUserPassword(UserWindow.signedInUser);

                // Confirmation Message
                UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Password Updated!");
            }
        }

        private void SubmitFeedbackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SQLqueriesObject.SaveFeedback(UserWindow.signedInUser.ID, RatingBar.Value , FeedbackTextBox.Text);

            // Confirmation Message
            UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Thank you for your feedback!");

        }

    }
}
