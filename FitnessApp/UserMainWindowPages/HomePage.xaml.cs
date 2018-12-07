using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows.Controls;
using System.Windows;

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

            // Setting Data context for ChallengesListBox
            ViewModels.ChallengesViewModel challengesDataContext = new ViewModels.ChallengesViewModel();
            challengesDataContext.JoinedChallengesViewModel();
            ChallengesListBox.DataContext = challengesDataContext;

            WeightChart.DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

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

        private void BreakfastCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void LunchCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void DinnerCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void WorkoutsCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void JoinChallengeButton_Click(object sender, RoutedEventArgs e)
        {
            UserMainWindow.UserMainWindowObject.UserMainWindowPagesListBox.SelectedIndex = 1;
        }
    }
}
