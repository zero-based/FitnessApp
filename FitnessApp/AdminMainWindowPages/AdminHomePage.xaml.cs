using FitnessApp.SQLdatabase;
using FitnessApp.ViewModels;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

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
            FeedbacksListBox.DataContext = new FeedbacksViewModel();
            DeleteUserListBox.DataContext = new UserViewModel();
        }

        public SeriesCollection SeriesCollection { get; set; }
        public List<String> Labels { get; set; }
        public Func<double, string> Formatter { get; set; }


        private void LoadAppRatingChart()
        {
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

        private void DeleteFeedbackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void AddNewAdminButton_Click(object sender, System.Windows.RoutedEventArgs e)
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

        private void UserSearchButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void DeleteUserButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

    }
}
