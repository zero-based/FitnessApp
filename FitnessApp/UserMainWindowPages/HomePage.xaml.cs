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
using System.Windows.Media;
using System.Linq;

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
            LoadTotalWeightLostCard(UserMainWindow.signedInUser.ID);
            LoadAverageWeightLostCard(UserMainWindow.signedInUser.ID);
            LoadJoinedChallengesCards(UserMainWindow.signedInUser.ID);
            LoadJoinedPlanCard(UserMainWindow.signedInUser.ID);
            LoadMotivationalQuoteCard();
            LoadCaloriesCard(UserMainWindow.signedInUser.ID);

            FoodComboBox.ItemsSource = SQLqueriesObject.GetAllFood();
            WorkoutsComboBox.ItemsSource = SQLqueriesObject.GetAllWorkouts();

        }


        ////////// All Weight Cards Functions/Event Handlers //////////

        // Weight Chart Properties

        public SeriesCollection WeightsSeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public void LoadWeightChart(int userID)
        {

            WeightsSeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Ideal Weight",
                    Values = Enumerable.Repeat(CalculateIdealWeight(), 10).AsChartValues(),
                    PointGeometry = null,
                    Fill = Brushes.Transparent,
                    Stroke = Brushes.ForestGreen,
                    StrokeDashArray = new DoubleCollection {3},
                },

                new LineSeries
                {
                    Title = "Weight",
                    Values = SQLqueriesObject.GetWeightValues(userID).AsChartValues(),
                },

                new LineSeries
                {
                    Title = "Target Weight",
                    Values = Enumerable.Repeat(UserMainWindow.signedInUser.TargetWeight, 10).AsChartValues(),
                    PointGeometry = null,
                    Fill = Brushes.Transparent,
                    Stroke = Brushes.Red,
                    StrokeDashArray = new DoubleCollection {3},
                }
                
            };

            Labels = SQLqueriesObject.GetWeightDateValues(userID);
            YFormatter = value => value.ToString() + " kg";

            // Setting Data context for Weight Chart
            WeightChart.DataContext = this;
        }

        private double CalculateIdealWeight()
        {
            if (UserMainWindow.signedInUser.Gender == "Male")
                return (UserMainWindow.signedInUser.Height - 100) + ((UserMainWindow.signedInUser.Height - 100) * 0.10);

            else
                return (UserMainWindow.signedInUser.Height - 100) + ((UserMainWindow.signedInUser.Height - 100) * 0.15);
        }

        private void DecimalNumbersOnlyFieldValidation(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            var text = s.Text.Insert(s.SelectionStart, e.Text);
            e.Handled = !double.TryParse(text, out double d);
        }

        private void SaveWeightButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TodaysWeightTextBox.Text))
                UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please enter your weight!");
            else
            {
                // Update Weight in Database
                SQLqueriesObject.AddNewWeight(double.Parse(TodaysWeightTextBox.Text), UserMainWindow.signedInUser.ID);

                // Update User Weight Line Series:
                // Add one value and remove another to keep the number of values 10
                WeightChart.Series[1].Values.Add(double.Parse(TodaysWeightTextBox.Text));
                WeightChart.Series[1].Values.RemoveAt(0);

                // Confirmation Message
                UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Weight added successfully");

                // Reset TextBox
                TodaysWeightTextBox.Text = "";

                // Refresh Weight-Related Cards
                LoadTotalWeightLostCard(UserMainWindow.signedInUser.ID);
                LoadAverageWeightLostCard(UserMainWindow.signedInUser.ID);

                // Refresh Calories Card
                LoadCaloriesCard(UserMainWindow.signedInUser.ID);
            }
        }

        public void LoadTotalWeightLostCard(int userID)
        {

            double WeightLostPerWeek = SQLqueriesObject.GetTotalWeightLostPerWeek(userID);
            double WeightLostPerMonth = SQLqueriesObject.GetTotalWeightLostPerMonth(userID);
            double WeightLostPerYear = SQLqueriesObject.GetTotalWeightLostPerYear(userID);


            // Set Colours
            if (WeightLostPerWeek < 0)
                WeightLostPerWeekTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            else
                WeightLostPerWeekTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];

            if (WeightLostPerWeek < 0)
                WeightLostPerMonthTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            else
                WeightLostPerMonthTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];

            if (WeightLostPerWeek < 0)
                WeightLostPerYearTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            else
                WeightLostPerYearTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];


            // Assign Values to TextBoxes
            WeightLostPerWeekTextBlock.Text = WeightLostPerWeek.ToString();
            WeightLostPerMonthTextBlock.Text = WeightLostPerMonth.ToString();
            WeightLostPerYearTextBlock.Text = WeightLostPerYear.ToString();

        }

        public void LoadAverageWeightLostCard(int userID)
        {

            double AverageWeightLostPerWeek = SQLqueriesObject.GetAverageWeightLostPerWeek(userID);
            double AverageWeightLostPerMonth = SQLqueriesObject.GetAverageWeightLostPerMonth(userID);
            double AverageWeightLostPerYear = SQLqueriesObject.GetAverageWeightLostPerYear(userID);


            // Set Colours
            if (AverageWeightLostPerWeek < 0)
                AverageWeightLostPerWeekTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            else
                AverageWeightLostPerWeekTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];

            if (AverageWeightLostPerMonth < 0)
                AverageWeightLostPerMonthTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            else
                AverageWeightLostPerMonthTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];

            if (AverageWeightLostPerYear < 0)
                AverageWeightLostPerYearTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            else
                AverageWeightLostPerYearTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];


            // Assign Values to TextBoxes
            AverageWeightLostPerWeekTextBlock.Text = AverageWeightLostPerWeek.ToString();
            AverageWeightLostPerMonthTextBlock.Text = AverageWeightLostPerMonth.ToString();
            AverageWeightLostPerYearTextBlock.Text = AverageWeightLostPerYear.ToString();
        }




        ////////// Joined Challenges Cards Functions/Event Handlers //////////


        // Setting Data context for JoinedChallengesListBox
        public void LoadJoinedChallengesCards(int userID)
        {
            ChallengesViewModel joinedChallengesDataContext = new ChallengesViewModel();
            joinedChallengesDataContext.JoinedChallengesViewModel(userID);
            CompletedJoinedChallengesListBox.DataContext = joinedChallengesDataContext;

            ChallengesViewModel uncompletedJoinedChallengesDataContext = new ChallengesViewModel();
            uncompletedJoinedChallengesDataContext.JoinedChallengesViewModel(userID);
            UncompletedJoinedChallengesListBox.DataContext = uncompletedJoinedChallengesDataContext;
            ControlNoChallengesCard(joinedChallengesDataContext);
        }


        private void JoinChallengeButton_Click(object sender, RoutedEventArgs e)
        {
            UserMainWindow.UserMainWindowObject.UserMainWindowPagesListBox.SelectedIndex = 1;
        }

        private void JoinChallengeButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            int selectedChallengeIndex = UncompletedJoinedChallengesListBox.Items.IndexOf(toggleButton.DataContext);

            ChallengeModel currentChallenge = (ChallengeModel)UncompletedJoinedChallengesListBox.Items[selectedChallengeIndex];

            SQLqueriesObject.UnjoinChallenge(UserMainWindow.signedInUser.ID, currentChallenge.ID);

            // Reloading Data context for JoinedChallengesListBox
            ChallengesViewModel joinedChallengesDataContext = new ChallengesViewModel();
            joinedChallengesDataContext.JoinedChallengesViewModel(UserMainWindow.signedInUser.ID);
            UncompletedJoinedChallengesListBox.DataContext = joinedChallengesDataContext;

            // Refresh Challenges Page
            UserMainWindow.ChallengesPageObject.LoadAllChallengesCards(UserMainWindow.signedInUser.ID);
        }

        private void ControlNoChallengesCard(ChallengesViewModel challengesViewModel)
        {
            if (challengesViewModel.UncompletedJoinedChallengeModels.Count > 0)
                NoChallengesCard.Visibility = Visibility.Collapsed;
            else
                NoChallengesCard.Visibility = Visibility.Visible;
        }

        private void CompletedChallengeButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int selectedChallengeIndex = CompletedJoinedChallengesListBox.Items.IndexOf(button.DataContext);

            ChallengeModel currentChallenge = (ChallengeModel)CompletedJoinedChallengesListBox.Items[selectedChallengeIndex];

            SQLqueriesObject.UnjoinChallenge(UserMainWindow.signedInUser.ID, currentChallenge.ID);

            LoadJoinedChallengesCards(UserMainWindow.signedInUser.ID);
        }



        ////////// Joined Plan Card Functions/Event Handlers //////////


        public void LoadJoinedPlanCard(int userID)
        {
            bool checkJoinedInPlan = SQLqueriesObject.IsInPlan(userID);

            NoPlanCard.Visibility = Visibility.Visible;
            JoinedPlanCard.Visibility = Visibility.Visible;
            PlanCompletedCard.Visibility = Visibility.Visible;

            if (checkJoinedInPlan)
            {
                NoPlanCard.Visibility = Visibility.Collapsed;

                int planDayNum = SQLqueriesObject.GetJoinedPlanDayNumber(userID);

                if (planDayNum > 30)
                    JoinedPlanCard.Visibility = Visibility.Collapsed;
                else
                {
                    PlanCompletedCard.Visibility = Visibility.Collapsed;

                    // Load Header
                    string planName = SQLqueriesObject.GetJoinedPlanName(userID).ToString();
                    PlanHeaderTextBlock.Text = planName + " | Day #" + planDayNum;
                    SQLqueriesObject.UpdatePlanDayNumber(userID, planDayNum);

                    // Load CheckBoxes
                    BreakfastCheckBox.IsChecked = SQLqueriesObject.GetDayBreakfastStatus(userID);
                    LunchCheckBox.IsChecked = SQLqueriesObject.GetDayLunchStatus(userID);
                    DinnerCheckBox.IsChecked = SQLqueriesObject.GetDayDinnerStatus(userID);
                    WorkoutsCheckBox.IsChecked = SQLqueriesObject.GetDayWorkoutStatus(userID);

                    // Load Descriptions
                    BreakfastDescriptionTextBlock.Text = SQLqueriesObject.GetDayBreakfastDescription(userID);
                    LunchDescriptionTextBlock.Text = SQLqueriesObject.GetDayLucnchDescription(userID);
                    DinnerDescriptionTextBlock.Text = SQLqueriesObject.GetDayDinnerDescription(userID);
                    WorkoutsDescriptionTextBlock.Text = SQLqueriesObject.GetDayWorkoutDescription(userID);

                    // Load Progress Bar
                    PlanProgressBar.Value = planDayNum;
                }
            }
            else
            {
                JoinedPlanCard.Visibility = Visibility.Collapsed;
                PlanCompletedCard.Visibility = Visibility.Collapsed;
            }
        }

        private void DayItemCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = sender as CheckBox;

            switch (currentCheckBox.Name)
            {
                case "BreakfastCheckBox":
                    SQLqueriesObject.UpdateDayBreakfastStatus(true, UserMainWindow.signedInUser.ID);
                    break;

                case "LunchCheckBox":
                    SQLqueriesObject.UpdateDayLunchStatus(true, UserMainWindow.signedInUser.ID);
                    break;

                case "DinnerCheckBox":
                    SQLqueriesObject.UpdateDayDinnerStatus(true, UserMainWindow.signedInUser.ID);
                    break;

                case "WorkoutsCheckBox":
                    SQLqueriesObject.UpdateDayWorkoutStatus(true, UserMainWindow.signedInUser.ID);
                    break;
            }
        }

        private void DayItemCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = sender as CheckBox;

            switch (currentCheckBox.Name)
            {
                case "BreakfastCheckBox":
                    SQLqueriesObject.UpdateDayBreakfastStatus(false, UserMainWindow.signedInUser.ID);
                    break;

                case "LunchCheckBox":
                    SQLqueriesObject.UpdateDayLunchStatus(false, UserMainWindow.signedInUser.ID);
                    break;

                case "DinnerCheckBox":
                    SQLqueriesObject.UpdateDayDinnerStatus(false, UserMainWindow.signedInUser.ID);
                    break;

                case "WorkoutsCheckBox":
                    SQLqueriesObject.UpdateDayWorkoutStatus(false, UserMainWindow.signedInUser.ID);
                    break;
            }
        }

        private void JoinPlanButton_Click(object sender, RoutedEventArgs e)
        {
            UserMainWindow.UserMainWindowObject.UserMainWindowPagesListBox.SelectedIndex = 2;
        }

        private void DismissPlanButton_Click(object sender, RoutedEventArgs e)
        {
            SQLqueriesObject.UnjoinPlan(UserMainWindow.signedInUser.ID);
            LoadJoinedPlanCard(UserMainWindow.signedInUser.ID);
            UserMainWindow.PlansPageObject.PlansListBox.DataContext = new PlansViewModel(UserMainWindow.signedInUser.ID);
        }


        //////////////////////////////////////////////////////////////




        ////////// Motivational Quotes Card Functions/Event Handlers //////////

        private void LoadMotivationalQuoteCard()
        {
            MotiationalQuoteTextBlock.Text = SQLqueriesObject.GetMotivationalQuote();
        }

        //////////////////////////////////////////////////////////////////////




        ////////// Calories Card Functions/Event Handlers //////////


        public void LoadCaloriesCard(int userID)
        {
            CaloriesGainedTextBlock.Text = SQLqueriesObject.GetCaloriesGainedToday(userID).ToString();
            CaloriesNeededTextBlock.Text = CalculateCaloriedNeeded().ToString();
            CaloriesLostTextBlock  .Text = SQLqueriesObject.GetCaloriesLostToday(userID).ToString();
        }

        private double CalculateCaloriedNeeded()
        {

            if (UserMainWindow.signedInUser.Gender == "Male")
                return  66 + (13.7 * UserMainWindow.signedInUser.LatestWeight)
                           + (1.8  * UserMainWindow.signedInUser.Height)
                           - (4.7  * UserMainWindow.signedInUser.Age);
            else
                return 665 + (9.6 * UserMainWindow.signedInUser.LatestWeight)
                           + (1.8 * UserMainWindow.signedInUser.Height)
                           - (4.7 * UserMainWindow.signedInUser.Age);

        }

        ///////////////////////////////////////////////////////////




        ////////// PopUpBox Functions/Event Handlers //////////

        private void AddFoodButton_Click(object sender, RoutedEventArgs e)
        {
            DialogBox.IsOpen = true;
            AddFoodDialogBox.Visibility = Visibility.Visible;
        }

        private void AddWorkoutButton_Click(object sender, RoutedEventArgs e)
        {
            DialogBox.IsOpen = true;
            AddWorkoutDialogBox.Visibility = Visibility.Visible;
        }

        //////////////////////////////////////////////////////




        ////////// DialogBoxes Functions/Event Handlers //////////

        private void DialogBoxAddFoodButton_Click(object sender, RoutedEventArgs e)
        {

            if (FoodComboBox.SelectedIndex == -1)
                UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please choose Food!");

            else if (FoodQuantityTextBox.Text == "")
                UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please enter Food Quantity!");

            else
            {
                SQLqueriesObject.AddFood(FoodComboBox.Text, double.Parse(FoodQuantityTextBox.Text), UserMainWindow.signedInUser.ID);
                AddFoodDialogBox.Visibility = Visibility.Collapsed;
                DialogBox.IsOpen = false;

                // Refresh Calories Card
                LoadCaloriesCard(UserMainWindow.signedInUser.ID);
            }

            // Reset Dialog Box Fields
            FoodComboBox.SelectedIndex = -1;
            FoodQuantityTextBox.Text = "";
        }

        private void DialogBoxAddWorkoutButton_Click(object sender, RoutedEventArgs e)
        {

            if (WorkoutsComboBox.SelectedIndex == -1)
                UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please choose Workout!");

            else if (WorkoutsDurationTextBox.Text == "")
                UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please enter Workout Duration!");

            else
            {
                SQLqueriesObject.AddWorkout(WorkoutsComboBox.Text, double.Parse(WorkoutsDurationTextBox.Text), UserMainWindow.signedInUser);
                AddWorkoutDialogBox.Visibility = Visibility.Collapsed;
                DialogBox.IsOpen = false;

                // Update Progress of the Challenges having the same type as the entered workout
                SQLqueriesObject.UpdateChallengesProgress(UserMainWindow.signedInUser.ID, WorkoutsComboBox.Text, double.Parse(WorkoutsDurationTextBox.Text));

                // Refresh Challenges card
                LoadJoinedChallengesCards(UserMainWindow.signedInUser.ID);

                // Refresh Calories Card
                LoadCaloriesCard(UserMainWindow.signedInUser.ID);
            }

            // Reset Dialog Box Fields
            WorkoutsComboBox.SelectedIndex = -1;
            WorkoutsDurationTextBox.Text = "";
        }

        private void DialogBoxCancelButton_Click(object sender, RoutedEventArgs e)
        {
            AddFoodDialogBox.Visibility = Visibility.Collapsed;
            AddWorkoutDialogBox.Visibility = Visibility.Collapsed;
            DialogBox.IsOpen = false;

            WorkoutsComboBox.SelectedIndex = -1;
            WorkoutsDurationTextBox.Text = "";

            FoodComboBox.SelectedIndex = -1;
            FoodQuantityTextBox.Text = "";
        }

        ///////////////////////////////////////////////////////////


    }
}
