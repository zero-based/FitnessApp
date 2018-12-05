using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for FitnessPlansPage.xaml
    /// </summary>
    public partial class FitnessPlansPage : Page
    {
        public static FitnessPlansPage FitnessPlansPageObject = new FitnessPlansPage();
        int selectedPlanIndex;

        public FitnessPlansPage()
        {
            InitializeComponent();
            FitnessPlansPageObject = this;
            PlansListBox.DataContext = new ViewModels.PlansViewModel();
        }

        private void ViewMoreButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PlanDaysListBox.DataContext = new ViewModels.DaysViewModel(selectedPlanIndex + 1);
            DaysSideDrawer.IsRightDrawerOpen = true;
        }

        private void JoinPlanButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ToggleButton toggleButton  = sender as ToggleButton;
            selectedPlanIndex = PlansListBox.Items.IndexOf(toggleButton.DataContext);

        }
    }
}
