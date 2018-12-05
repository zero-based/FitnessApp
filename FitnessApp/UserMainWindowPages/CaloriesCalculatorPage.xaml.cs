using System.Windows.Controls;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for CaloriesCalculatorPage.xaml
    /// </summary>
    public partial class CaloriesCalculatorPage : Page
    {
        public static CaloriesCalculatorPage CaloriesCalculatorPageObject = new CaloriesCalculatorPage();

        public CaloriesCalculatorPage()
        {
            InitializeComponent();
            CaloriesCalculatorPageObject = this;
        }

        private void CalculateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
