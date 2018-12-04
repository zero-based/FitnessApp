using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows.Controls;

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

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private void SaveWeightButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void JoinPlanButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
