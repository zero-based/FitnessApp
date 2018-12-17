using FitnessApp.Models;
using FitnessApp.SQLdatabase;
using FitnessApp.ViewModels;
using FitnessApp.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace FitnessApp.UserWindowPages
{
    /// <summary>
    /// Interaction logic for ChallengesPage.xaml
    /// </summary>
    public partial class ChallengesPage : Page
    {

        public ChallengesPage()
        {
            InitializeComponent();
            UserWindow.ChallengesPageObject = this;

            LoadAllChallengesCards();
        }

        public void LoadAllChallengesCards()
        {
            // Setting Data context for ChallengesListBox
            ChallengesViewModel challengesDataContext = new ChallengesViewModel();
            challengesDataContext.AllChallengesViewModel(UserWindow.signedInUser.ID);
            DataContext = challengesDataContext;
        }

        private void JoinChallengeButton_Checked(object sender, RoutedEventArgs e)
        {

            ToggleButton toggleButton = sender as ToggleButton;
            int selectedChallengeIndex = ChallengesListBox.Items.IndexOf(toggleButton.DataContext);

            ChallengeModel currentChallenge = (ChallengeModel) ChallengesListBox.Items[selectedChallengeIndex];

            SQLqueries.JoinChallenge(UserWindow.signedInUser.ID, currentChallenge.ID);

            // Rrefresh Joined Challenges Cards in Home Page 
            UserWindow.HomePageObject.LoadJoinedChallengesCards();
        }
            
        private void JoinChallengeButton_Unchecked(object sender, RoutedEventArgs e)
        {

            ToggleButton toggleButton = sender as ToggleButton;
            int selectedChallengeIndex = ChallengesListBox.Items.IndexOf(toggleButton.DataContext);

            ChallengeModel currentChallenge = (ChallengeModel) ChallengesListBox.Items[selectedChallengeIndex];

            SQLqueries.UnjoinChallenge(UserWindow.signedInUser.ID, currentChallenge.ID);

            // Rrefresh Joined Challenges Cards in Home Page 
            UserWindow.HomePageObject.LoadJoinedChallengesCards();
        }
    }

}

