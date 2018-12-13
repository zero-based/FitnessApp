using FitnessApp.SQLdatabase;
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
            LoadWorkoutTypeComboBox();
        }
        private void TextBoxesNumbersOnly_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
        private void ChallengeDialogBoxAddButton_Click(object sender, RoutedEventArgs e)
        {
            DialogBox.IsOpen = true;
            AddChallengeDialogBox.Visibility = Visibility.Visible;
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

        private void AddChallengePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            DialogBox.IsOpen = false;
            AddChallengeDialogBox.Visibility = Visibility.Collapsed;
        }

        private void LoadWorkoutTypeComboBox()
        {
            List<string> items = new List<string>();
            items.Add("new-item");
            WorkoutTypeComboBox.ItemsSource = items;
        }
    }
}
