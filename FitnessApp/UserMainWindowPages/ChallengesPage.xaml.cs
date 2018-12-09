using FitnessApp.ViewModels;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

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

            // Setting Data context for ChallengesListBox
            ChallengesViewModel challengesDataContext = new ChallengesViewModel();
            challengesDataContext.AllChallengesViewModel(100);
            DataContext = challengesDataContext;
        }

        private void JoinChallengeButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            //use selectedChallengeIndex + 1 as Challenge ID while using database
            ToggleButton toggleButton = sender as ToggleButton;
            selectedChallengeIndex = ChallengesListBox.Items.IndexOf(toggleButton.DataContext);

            Models.ChallengeModel joinedChallenge = (Models.ChallengeModel)ChallengesListBox.Items[selectedChallengeIndex];

            //To Get Joined Challenge ID use: joinedChallenge.ID
        }
    }
}
