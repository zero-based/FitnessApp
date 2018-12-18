using FitnessApp.Models;
using FitnessApp.SQLserver;
using FitnessApp.ViewModels;
using FitnessApp.Windows;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FitnessApp.AdminWindowPages
{
    /// <summary>
    /// Interaction logic for ChallengesSetupPage.xaml
    /// </summary>
    public partial class ChallengesSetupPage : Page
    {
        private ImageModel challengePhoto = new ImageModel();

        public ChallengesSetupPage()
        {
            InitializeComponent();
            AdminWindow.ChallengesSetupPageObject = this;
            LoadAllChallenges();
            LoadWorkoutTypeComboBox();
        }

        // All Challenges

        private void LoadAllChallenges()
        {

            ChallengesViewModel challengesDataContext = new ChallengesViewModel();
            challengesDataContext.AllChallengesViewModel(0);
            AllChallengesListBox.DataContext = challengesDataContext;

        }

        private void DeleteChallengeButton_Click(object sender, RoutedEventArgs e)
        {
            // Get Challenge Index
            Button deleteButton = sender as Button;
            int selectedChallengeIndex = AllChallengesListBox.Items.IndexOf(deleteButton.DataContext);
            ChallengeModel chosenChallenge = (ChallengeModel)AllChallengesListBox.Items[selectedChallengeIndex];

            // Delete Challenge From Database
            Database.DeleteChallenge(chosenChallenge.ID);

            // Refresh Challenges
            LoadAllChallenges();

            // Confirmation Message
            AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Challenge deleted successfully");
        }


        // DialogBox Functions

        private void DecimalNumbersOnlyFieldValidation(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            var text = s.Text.Insert(s.SelectionStart, e.Text);
            e.Handled = !double.TryParse(text, out double d);
        }

        private void LoadWorkoutTypeComboBox()
        {
            // Get All Workouts From Database
            WorkoutTypeComboBox.ItemsSource = Database.GetAllWorkouts();
        }

        private void ChallengeDialogBoxAddButton_Click(object sender, RoutedEventArgs e)
        {
            DialogBox.IsOpen = true;
            AddChallengeDialogBox.Visibility = Visibility.Visible;
        }

        private void DialogBoxAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ChallengeNameTextBox        .Text) ||
               string.IsNullOrWhiteSpace(ChallengeDescriptionTextBox  .Text) ||
               string.IsNullOrWhiteSpace(ChallengeDueDatePicker       .Text) ||
               WorkoutTypeComboBox.SelectedIndex == -1                       ||
               string.IsNullOrWhiteSpace(ChallengeTargetMinutesTextBox.Text) ||
               string.IsNullOrWhiteSpace(ChallengeRewardTextBox.Text))
            {
                AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("All Fields are Required");
            }

            else
            {
                // Add Challenge to databse
                Database.AddNewChallenge(challengePhoto.ByteArray,
                                                 ChallengeNameTextBox.Text,
                                                 ChallengeDescriptionTextBox.Text,
                                                 int.Parse(ChallengeTargetMinutesTextBox.Text),
                                                 ChallengeRewardTextBox.Text,
                                                 ChallengeDueDatePicker.SelectedDate,
                                                 Database.GetWorkoutID(WorkoutTypeComboBox.Text));

                AddChallengeDialogBox.Visibility = Visibility.Collapsed;
                DialogBox.IsOpen = false;
                ResetDialogBox();

                // Refresh Challenges
                LoadAllChallenges();

                // Confirmation Message
                AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Challenge added successfully");
            }
                
        }

        private void DialogBoxCancelButton_Click(object sender, RoutedEventArgs e)
        {
            AddChallengeDialogBox.Visibility = Visibility.Collapsed;
            DialogBox.IsOpen = false;

            ResetDialogBox();
        }

        private void AddChallengePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog browsePhotoDialog = new OpenFileDialog();
            browsePhotoDialog.Title = "Select Challenge Photo";
            browsePhotoDialog.Filter = "All formats|*.jpg;*.jpeg;*.png|" +
                                       "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                       "PNG (*.png)|*.png";

            if (browsePhotoDialog.ShowDialog() == true)
            {
                challengePhoto = new ImageModel(browsePhotoDialog.FileName);

                // Confirmation Message
                AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Photo added successfully");
            }
        }

        private void ResetDialogBox()
        {
            ChallengeNameTextBox.Text = "";
            ChallengeDescriptionTextBox.Text = "";
            ChallengeDueDatePicker.Text = "";
            WorkoutTypeComboBox.SelectedIndex = -1;
            ChallengeTargetMinutesTextBox.Text = "";
            ChallengeRewardTextBox.Text = "";
            challengePhoto = new ImageModel();
        }
    }
}
