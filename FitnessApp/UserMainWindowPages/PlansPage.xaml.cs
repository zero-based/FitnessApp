using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using FitnessApp.Models;
using FitnessApp.ViewModels;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for PlansPage.xaml
    /// </summary>
    public partial class PlansPage : Page
    {
        public static PlansPage PlansPageObject = new PlansPage();

        public PlansPage()
        {
            InitializeComponent();
            PlansPageObject = this;
            PlansListBox.DataContext = new PlansViewModel(101);
        }

        private void ViewMoreButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;
            int selectedPlanIndex = PlansListBox.Items.IndexOf(button.DataContext);
            PlanModel currentPlan = (PlanModel)PlansListBox.Items[selectedPlanIndex];

            PlanDaysListBox.DataContext = new DaysViewModel(currentPlan.ID);
            DaysSideDrawer.IsRightDrawerOpen = true;
        }

        private void JoinPlanButton_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            int selectedPlanIndex = PlansListBox.Items.IndexOf(toggleButton.DataContext);
            PlanModel currentPlan = (PlanModel)PlansListBox.Items[selectedPlanIndex];
        }

        private void JoinPlanButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            int selectedPlanIndex = PlansListBox.Items.IndexOf(toggleButton.DataContext);
            PlanModel currentPlan = (PlanModel)PlansListBox.Items[selectedPlanIndex];
        }
    }
}
