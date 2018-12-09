using FitnessApp.ViewModels;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using FitnessApp.SQLdatabase;
using System.Windows;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for ChallengesPage.xaml
    /// </summary>
    public partial class ChallengesPage : Page
    {
        public static ChallengesPage ChallengesPageObject = new ChallengesPage();
        SQLqueries SQLqueriesObject = new SQLqueries();
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

            ToggleButton toggleButton = sender as ToggleButton;
            selectedChallengeIndex = ChallengesListBox.Items.IndexOf(toggleButton.DataContext);

            Models.ChallengeModel currentChallenge = (Models.ChallengeModel)ChallengesListBox.Items[selectedChallengeIndex];

            SQLqueriesObject.JoinChallenge(101, currentChallenge.ID);
        }
            
            
        

        private void JoinChallengeButton_Unchecked(object sender, RoutedEventArgs e)
        {

            ToggleButton toggleButton = sender as ToggleButton;
            selectedChallengeIndex = ChallengesListBox.Items.IndexOf(toggleButton.DataContext);

            Models.ChallengeModel currentChallenge = (Models.ChallengeModel)ChallengesListBox.Items[selectedChallengeIndex];

            SQLqueriesObject.UnjoinChallenge(101, currentChallenge.ID);

        }
    }

}

