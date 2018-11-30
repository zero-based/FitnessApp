using System.Windows.Controls;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for ChallengesPage.xaml
    /// </summary>
    public partial class ChallengesPage : Page
    {
        public static ChallengesPage ChallengesPageObject = new ChallengesPage();

        public ChallengesPage()
        {
            InitializeComponent();
            ChallengesPageObject = this;
            DataContext = new ViewModels.ChallengesViewModel();
        }
    }
}
