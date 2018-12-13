using LiveCharts.Wpf;
using LiveCharts;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using System;
using System.Windows.Controls.Primitives;
using FitnessApp.SQLdatabase;
using FitnessApp.Models;
using FitnessApp.ViewModels;
using System.Collections.Generic;
using LiveCharts.Helpers;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        SQLqueries SQLqueriesObject = new SQLqueries();

        public HomePage()
        {
            InitializeComponent();
            UserMainWindow.HomePageObject = this;

            LoadWeightChart(UserMainWindow.signedInUser.ID);
            LoadMotivationalQuoteCard();

            // Setting Data context for JoinedChallengesListBox
            ChallengesViewModel joinedChallengesDataContext = new ChallengesViewModel();
            joinedChallengesDataContext.JoinedChallengesViewModel(UserMainWindow.signedInUser.ID);
            JoinedChallengesListBox.DataContext = joinedChallengesDataContext;
            ControlNoChallengesCard(joinedChallengesDataContext);   
        }


        // Weight Chart Properties

        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }




        ////////// All Weight Cards Functions/Event Handlers //////////

        private void LoadWeightChart(int userID)
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Weight",
                    Values = SQLqueriesObject.GetWeightValues(userID).AsChartValues()
                }
            };

            Labels = SQLqueriesObject.GetWeightDateValues(userID);
            YFormatter = value => value.ToString();
            // Setting Data context for WeightChart
            WeightChart.DataContext = this;
        }

        private void DecimalNumbersOnlyFieldValidation(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            var text = s.Text.Insert(s.SelectionStart, e.Text);
            e.Handled = !double.TryParse(text, out double d);
        }

        private void SaveWeightButton_Click(object sender, RoutedEventArgs e)
        {
            SQLqueriesObject.AddNewWeight(double.Parse(TodaysWeightTextBox.Text), UserMainWindow.signedInUser.ID);
            WeightChart.Series[0].Values.Add(double.Parse(TodaysWeightTextBox.Text));
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

            SQLqueriesObject.UnjoinChallenge(UserMainWindow.signedInUser.ID, currentChallenge.ID);

            // Reloading Data context for JoinedChallengesListBox
            ChallengesViewModel joinedChallengesDataContext = new ChallengesViewModel();
            joinedChallengesDataContext.JoinedChallengesViewModel(UserMainWindow.signedInUser.ID);
            JoinedChallengesListBox.DataContext = joinedChallengesDataContext;
        }

        private void ControlNoChallengesCard(ChallengesViewModel challengesViewModel)
        {
            if (challengesViewModel.JoinedChallengeModels.Count > 0)
                NoChallengesCard.Visibility = Visibility.Collapsed;
            else
                NoChallengesCard.Visibility = Visibility.Visible;
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

        private void LoadMotivationalQuoteCard()
        {
            MotiationalQuoteTextBlock.Text = SQLqueriesObject.GetMotivationalQuote();
        }

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
