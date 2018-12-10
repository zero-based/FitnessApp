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
            PlansListBox.DataContext = new ViewModels.PlansViewModel();
        }

        private void ViewMoreButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PlanDaysListBox.DataContext = new ViewModels.DaysViewModel(selectedPlanIndex + 1);
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
