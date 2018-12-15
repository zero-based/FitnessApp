using FitnessApp.SQLdatabase;
using FitnessApp.ViewModels;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;


namespace FitnessApp.AdminMainWindowPages
{
    /// <summary>
    /// Interaction logic for ChallengesSetupPage.xaml
    /// </summary>
    public partial class ChallengesSetupPage : Page
    {
        public static ChallengesSetupPage ChallengesSetupPageObject = new ChallengesSetupPage();
        SQLqueries SQLqueriesObject = new SQLqueries();

        public ChallengesSetupPage()
        {
            InitializeComponent();
            LoadAllChallenges();
            LoadWorkoutTypeComboBox();
        }

        private void TextBoxesNumbersOnly_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }


        // DialogBox Functions
        private void ChallengeDialogBoxAddButton_Click(object sender, RoutedEventArgs e)
        {
            DialogBox.IsOpen = true;
            AddChallengeDialogBox.Visibility = Visibility.Visible;
        }

        private void LoadWorkoutTypeComboBox()
        {
            List<string> items = new List<string>();
            // GetAllWorkouts Here From Database
            WorkoutTypeComboBox.ItemsSource = items;
        }

        private void LoadAllChallenges()
        {
            ChallengesViewModel challengesDataContext = new ChallengesViewModel();
            challengesDataContext.AllChallengesViewModel(0);
            AllChallengesListBox.DataContext = challengesDataContext;

        }

        private void AddChallengePhotoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DialogBoxCancelButton_Click(object sender, RoutedEventArgs e)
        {
            AddChallengeDialogBox.Visibility = Visibility.Collapsed;
            DialogBox.IsOpen = false;
        }

        private void DialogBoxAddButton_Click(object sender, RoutedEventArgs e)
        {
            DialogBox.IsOpen = false;
            AddChallengeDialogBox.Visibility = Visibility.Collapsed;
        }


        // ListBox Functions
        private void DeleteChallengeButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void LoadAllChallengesListBOx()
        {

        }
    }
}
