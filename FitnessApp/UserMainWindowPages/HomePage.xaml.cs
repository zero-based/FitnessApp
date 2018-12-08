using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows.Controls;
using System.Windows;
using System.Text.RegularExpressions;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public static HomePage HomePageObject = new HomePage();

        public HomePage()
        {
            InitializeComponent();
            HomePageObject = this;

            
            SeriesCollection = new SeriesCollection
        {
            new LineSeries
            {
                Title = "Weight",
                Values = new ChartValues<double> { 40, 41.5, 39}
            },
        };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString();

            // Setting Data context for JoinedChallengesListBox
            ViewModels.ChallengesViewModel joinedChallengesDataContext = new ViewModels.ChallengesViewModel();
            joinedChallengesDataContext.JoinedChallengesViewModel();
            JoinedChallengesListBox.DataContext = joinedChallengesDataContext;

            WeightChart.DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }


        private void TextBoxesNumbersOnly_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void SaveWeightButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void JoinPlanButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserMainWindow.UserMainWindowObject.UserMainWindowPagesListBox.SelectedIndex = 2;
        }

        //PopUpBox event handlers
        private void AddMealButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogBox.IsOpen = true;
            AddMealDialogBox.Visibility = Visibility.Visible;
        }

        private void AddWorkoutButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogBox.IsOpen = true;
            AddWorkoutDialogBox.Visibility = Visibility.Visible;
        }

        //Add Meal/Workout event handlers
        private void DialogBoxAddMealButton_Click(object sender, RoutedEventArgs e)
        {
            //Adding Meal code Here
            AddMealDialogBox.Visibility = Visibility.Collapsed;
            DialogBox.IsOpen = false;
        }

        private void DialogBoxAddWorkoutButton_Click(object sender, RoutedEventArgs e)
        {
            //Adding Workout code Here
            AddWorkoutDialogBox.Visibility = Visibility.Collapsed;
            DialogBox.IsOpen = false;
        }

        //Cancel DialogBox
        private void DialogBoxCancelButton_Click(object sender, RoutedEventArgs e)
        {
            AddMealDialogBox.Visibility = Visibility.Collapsed;
            AddWorkoutDialogBox.Visibility = Visibility.Collapsed;
            DialogBox.IsOpen = false;
        }


        private void JoinChallengeButton_Click(object sender, RoutedEventArgs e)
        {
            UserMainWindow.UserMainWindowObject.UserMainWindowPagesListBox.SelectedIndex = 1;
        }


        // Joined Plan CheckBoxes Checked/Unchecked event Handlers
        private void DayItemCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = sender as CheckBox;
            MessageBox.Show(currentCheckBox.Name.ToString() + "is: " + currentCheckBox.IsChecked.ToString());
        }

        private void DayItemCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = sender as CheckBox;
            MessageBox.Show(currentCheckBox.Name.ToString() + "is: " + currentCheckBox.IsChecked.ToString());
        }
    }
}
