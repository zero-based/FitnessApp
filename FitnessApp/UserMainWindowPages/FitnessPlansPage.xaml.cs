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
        }
    }
}
