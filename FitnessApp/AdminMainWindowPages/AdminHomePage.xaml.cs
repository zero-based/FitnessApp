using FitnessApp.SQLdatabase;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows.Controls;

namespace FitnessApp.AdminMainWindowPages
{
    /// <summary>
    /// Interaction logic for AdminHomePage.xaml
    /// </summary>
    public partial class AdminHomePage : Page
    {
        public static AdminHomePage AdminHomePageObject = new AdminHomePage();
        SQLqueries SQLqueriesObject = new SQLqueries();

        public AdminHomePage()
        {
            InitializeComponent();

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
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}
