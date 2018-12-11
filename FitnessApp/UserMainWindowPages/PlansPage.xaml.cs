using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using FitnessApp.Models;
using FitnessApp.ViewModels;
using FitnessApp.SQLdatabase;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for PlansPage.xaml
    /// </summary>
    public partial class PlansPage : Page
    {
        public static PlansPage PlansPageObject = new PlansPage();
        SQLqueries SQLqueriesObject = new SQLqueries();

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


        private void JoinPlanButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            int selectedPlanIndex = PlansListBox.Items.IndexOf(toggleButton.DataContext);
            PlanModel currentPlan = (PlanModel)PlansListBox.Items[selectedPlanIndex];

            if (SQLqueriesObject.IsInPlan(101))
                UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("You are currently in a plan. Please unjoin it first.");
            else
                SQLqueriesObject.JoinPlan(101, currentPlan.ID);

            PlansListBox.DataContext = new PlansViewModel(101);
        }

        private void JoinPlanButton_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            int selectedPlanIndex = PlansListBox.Items.IndexOf(toggleButton.DataContext);
            PlanModel currentPlan = (PlanModel)PlansListBox.Items[selectedPlanIndex];

            SQLqueriesObject.UnjoinPlan(101);

            PlansListBox.DataContext = new PlansViewModel(101);
        }
    }
}
