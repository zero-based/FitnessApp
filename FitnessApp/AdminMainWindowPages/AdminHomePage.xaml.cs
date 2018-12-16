using FitnessApp.Models;
using FitnessApp.SQLdatabase;
using FitnessApp.ViewModels;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;

namespace FitnessApp.AdminMainWindowPages
{
    /// <summary>
    /// Interaction logic for AdminHomePage.xaml
    /// </summary>
    public partial class AdminHomePage : Page
    {
        SQLqueries SQLqueriesObject = new SQLqueries();

        public AdminHomePage()
        {
            InitializeComponent();
            AdminMainWindow.AdminHomePageObject = this;

            LoadAppRatingChart();
            LoadAppUsersNumber();
            FeedbacksListBox.DataContext = new FeedbacksViewModel();
        }

        public SeriesCollection SeriesCollection { get; set; }
        public List<String> Labels { get; set; }
        public Func<double, string> Formatter { get; set; }


        private void LoadAppRatingChart()
        {
            AppRatingChart.DataContext = null;
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Rating",
                    Values = SQLqueriesObject.GetAppRatingValues().AsChartValues()
                }
            };
            Labels = new List<string> { "1", "2", "3", "4", "5" };
            Formatter = value => value.ToString("N");

            AppRatingChart.DataContext = this;
        }

        private void LoadAppUsersNumber()
        {
            AppUsersNumberTextBlock.Text = SQLqueriesObject.GetAppUsersNumber().ToString();
        }

        private void DeleteFeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteFeedbackButton = sender as Button;
            int selectedFeedbackIndex = FeedbacksListBox.Items.IndexOf(deleteFeedbackButton.DataContext);

            FeedbackModel chosenFeedback = (FeedbackModel)FeedbacksListBox.Items[selectedFeedbackIndex];
            SQLqueriesObject.DeleteFeedback(chosenFeedback.Feedback);

            FeedbacksListBox.DataContext = null;
            FeedbacksListBox.DataContext = new FeedbacksViewModel();
            LoadAppRatingChart();
        }

        private void AddNewAdminButton_Click(object sender, RoutedEventArgs e)
        {
            if (FirstNameTextBox.Text == "" || LastNameTextBox.Text == "" || NewAdminEmailTextBox.Text == "")
            {
                if (FirstNameTextBox.Text == "")
                    AdminMainWindow.AdminMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("First Name Is Empty!");
                if (LastNameTextBox.Text == "")
                    AdminMainWindow.AdminMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Last Name Is Empty!");
                if (NewAdminEmailTextBox.Text == "")
                    AdminMainWindow.AdminMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Email Is Empty!");
            }
                // Check Email Validation
            else if (!NewAdminEmailTextBox.Text.Contains("@") || !NewAdminEmailTextBox.Text.Contains(".com"))
                AdminMainWindow.AdminMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Invalid E-mail");
            else
            {
                SQLqueriesObject.AddNewAdmin(NewAdminEmailTextBox.Text, FirstNameTextBox.Text, LastNameTextBox.Text);
                AdminMainWindow.AdminMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Admin Added Succesfully");
            }
        }

        private void UserSearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(UserSearchTextBox.Text))
            {
                UserViewModel deletedUserDataContext = new UserViewModel(UserSearchTextBox.Text);
                DeleteUserListBox.DataContext = deletedUserDataContext;

                // Show card or Error message; depending on number of users
                if (deletedUserDataContext.UserModels.Count > 0)
                    DeleteUsersCard.Visibility = Visibility.Visible;
                else
                    AdminMainWindow.AdminMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("No users found");
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Get User ID
            Button deleteButton = sender as Button;
            int selectedUserIndex = DeleteUserListBox.Items.IndexOf(deleteButton.DataContext);
            UserModel chosenUser = (UserModel)DeleteUserListBox.Items[selectedUserIndex];

            // Delete Challenge From Database
            SQLqueriesObject.DeleteUser(chosenUser.ID);

            // Refresh Listbox and Number of users
            DeleteUserListBox.DataContext = null;
            UserViewModel deletedUserDataContext = new UserViewModel(UserSearchTextBox.Text);
            DeleteUserListBox.DataContext = deletedUserDataContext;
            AppUsersNumberTextBlock.Text = SQLqueriesObject.GetAppUsersNumber().ToString();

            // Hide DeleteUsersCard if no remaining users exist 
            if (deletedUserDataContext.UserModels.Count == 0)
                DeleteUsersCard.Visibility = Visibility.Collapsed;

            // Confirmation Message
            AdminMainWindow.AdminMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("User deleted successfully");

        }

    }
}
