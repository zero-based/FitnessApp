using FitnessApp.SQLdatabase;
using FitnessApp.ViewModels;
using LiveCharts;
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

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Rating",
                    Values = new ChartValues<double> { 10, 50, 39, 50, 40 }
                }
            };
            Labels = new[] { "1", "2", "3", "4","5" };
            Formatter = value => value.ToString("N");

            FeedbackRatingChart.DataContext = this;

            FeedbacksListBox.DataContext = new FeedbacksViewModel();

            DeleteUserListBox.DataContext = new UserViewModel();
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void DeleteFeedbackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void AddNewAdminButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void UserSearchButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void DeleteUserButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

    }
}
