using LiveCharts.Wpf;
using LiveCharts;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows;
using System;
using System.Windows.Controls.Primitives;
using FitnessApp.SQLdatabase;
using FitnessApp.Models;
using FitnessApp.ViewModels;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public static HomePage HomePageObject = new HomePage();
        SQLqueries SQLqueriesObject = new SQLqueries();

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
                }
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString();

            // Setting Data context for JoinedChallengesListBox
            ChallengesViewModel joinedChallengesDataContext = new ChallengesViewModel();
            joinedChallengesDataContext.JoinedChallengesViewModel(100);
            JoinedChallengesListBox.DataContext = joinedChallengesDataContext;

            // Setting Data context for WeightChart
            WeightChart.DataContext = this;
        }


        // Weight Chart Properties

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }




        ////////// All Weight Cards Functions/Event Handlers //////////

        private void TextBoxesNumbersOnly_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void SaveWeightButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }




        ////////// Joined Challenges Cards Functions/Event Handlers //////////

        private void JoinChallengeButton_Click(object sender, RoutedEventArgs e)
        {
            UserMainWindow.UserMainWindowObject.UserMainWindowPagesListBox.SelectedIndex = 1;
        }

        private void JoinChallengeButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            int selectedChallengeIndex = JoinedChallengesListBox.Items.IndexOf(toggleButton.DataContext);

            ChallengeModel currentChallenge = (ChallengeModel) JoinedChallengesListBox.Items[selectedChallengeIndex];

            SQLqueriesObject.UnjoinChallenge(101, currentChallenge.ID);

            // Reloading Data context for JoinedChallengesListBox
            ChallengesViewModel joinedChallengesDataContext = new ChallengesViewModel();
            joinedChallengesDataContext.JoinedChallengesViewModel(100);
            JoinedChallengesListBox.DataContext = joinedChallengesDataContext;
        }




        ////////// Joined Plan Card Functions/Event Handlers //////////

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

        private void JoinPlanButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserMainWindow.UserMainWindowObject.UserMainWindowPagesListBox.SelectedIndex = 2;
        }

        //////////////////////////////////////////////////////////////




        ////////// Motivational Quotes Card Functions/Event Handlers //////////


        //////////////////////////////////////////////////////////////////////




        ////////// Calories Card Functions/Event Handlers //////////


        ///////////////////////////////////////////////////////////




        ////////// PopUpBox Functions/Event Handlers //////////

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

        //////////////////////////////////////////////////////




        ////////// DialogBoxes Functions/Event Handlers //////////

        private void DialogBoxAddMealButton_Click(object sender, RoutedEventArgs e)
        {
            // Adding Meal code Here...
            AddMealDialogBox.Visibility = Visibility.Collapsed;
            DialogBox.IsOpen = false;
        }

        private void DialogBoxAddWorkoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Adding Workout code Here...
            AddWorkoutDialogBox.Visibility = Visibility.Collapsed;
            DialogBox.IsOpen = false;
        }

        private void DialogBoxCancelButton_Click(object sender, RoutedEventArgs e)
        {
            AddMealDialogBox.Visibility = Visibility.Collapsed;
            AddWorkoutDialogBox.Visibility = Visibility.Collapsed;
            DialogBox.IsOpen = false;
        }

        
        ///////////////////////////////////////////////////////////


    }
}
