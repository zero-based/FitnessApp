using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for ChallengesPage.xaml
    /// </summary>
    public partial class ChallengesPage : Page
    {
        public static ChallengesPage ChallengesPageObject = new ChallengesPage();

        int selectedChallengeIndex;

        public ChallengesPage()
        {
            InitializeComponent();
            ChallengesPageObject = this;
            DataContext = new ViewModels.ChallengesViewModel();
        }

        private void JoinChallengeButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            //use selectedChallengeIndex + 1 as Challenge ID while using database
            ToggleButton toggleButton = sender as ToggleButton;
            selectedChallengeIndex = ChallengesListBox.Items.IndexOf(toggleButton.DataContext);
        }
    }
}
