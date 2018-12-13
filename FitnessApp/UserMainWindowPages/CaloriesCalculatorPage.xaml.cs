using System.Windows.Controls;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for CaloriesCalculatorPage.xaml
    /// </summary>
    public partial class CaloriesCalculatorPage : Page
    {

        public CaloriesCalculatorPage()
        {
            InitializeComponent();
            UserMainWindow.CaloriesCalculatorPageObject = this;

            // Initialize DataContext with signedInUser Model
            DataContext = UserMainWindow.signedInUser;
        }

        private void CalculateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (GenderComboBox.Text == ("Female"))
            {

                double femaleCalculate;
                double weight = double.Parse(WeightTextBox.Text);
                double height = double.Parse(HeightTextBox.Text);
                double age = double.Parse(AgeTextBox.Text);
                femaleCalculate = 665 + (9.6 * (weight)) + (1.8 * (height)) - (4.7 * (age));
                ResultTextBlock.Text = femaleCalculate.ToString() + " kCal.";

            }
            else
            {

                double maleCalculate;
                double weight = double.Parse(WeightTextBox.Text);
                double height = double.Parse(HeightTextBox.Text);
                double age = double.Parse(AgeTextBox.Text);
                maleCalculate = 66 + (13.7 * (weight)) + (1.8 * (height)) - (4.7 * (age));
                ResultTextBlock.Text = maleCalculate.ToString() + " kCal.";

            }
        }

    }
}
