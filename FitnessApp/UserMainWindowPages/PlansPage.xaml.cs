using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for PlansPage.xaml
    /// </summary>
    public partial class PlansPage : Page
    {
        public static PlansPage PlansPageObject = new PlansPage();
        int selectedPlanIndex;

        public PlansPage()
        {
            InitializeComponent();
            PlansPageObject = this;
            PlansListBox.DataContext = new ViewModels.PlansViewModel(101);
        }

        private void ViewMoreButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;
            selectedPlanIndex = PlansListBox.Items.IndexOf(button.DataContext);
            Models.PlanModel currentPlan = (Models.PlanModel)PlansListBox.Items[selectedPlanIndex];

            PlanDaysListBox.DataContext = new ViewModels.DaysViewModel(currentPlan.ID);
            DaysSideDrawer.IsRightDrawerOpen = true;
        }

        private void JoinPlanButton_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            selectedPlanIndex = PlansListBox.Items.IndexOf(toggleButton.DataContext);
            Models.PlanModel currentPlan = (Models.PlanModel)PlansListBox.Items[selectedPlanIndex];
        }

        private void JoinPlanButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            selectedPlanIndex = PlansListBox.Items.IndexOf(toggleButton.DataContext);
            Models.PlanModel currentPlan = (Models.PlanModel)PlansListBox.Items[selectedPlanIndex];
        }
    }
}
