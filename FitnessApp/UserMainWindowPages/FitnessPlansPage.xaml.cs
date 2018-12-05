using System.Windows.Controls;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for FitnessPlansPage.xaml
    /// </summary>
    public partial class FitnessPlansPage : Page
    {
        public static FitnessPlansPage FitnessPlansPageObject = new FitnessPlansPage();

        public FitnessPlansPage()
        {
            InitializeComponent();
            FitnessPlansPageObject = this;
            DataContext = new ViewModels.PlansViewModel();
        }

        private void OpenPlanDaysButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DaysSideDrawer.IsRightDrawerOpen = true;
        }
    }
}
