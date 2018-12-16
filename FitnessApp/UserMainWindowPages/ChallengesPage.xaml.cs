using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using FitnessApp.SQLdatabase;
using System.Windows;
using FitnessApp.Models;
using FitnessApp.ViewModels;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for ChallengesPage.xaml
    /// </summary>
    public partial class ChallengesPage : Page
    {
        SQLqueries SQLqueriesObject = new SQLqueries();

        public ChallengesPage()
        {
            InitializeComponent();
            UserMainWindow.ChallengesPageObject = this;

            LoadAllChallengesCards();
        }

        public void LoadAllChallengesCards()
        {
            // Setting Data context for ChallengesListBox
            ChallengesViewModel challengesDataContext = new ChallengesViewModel();
            challengesDataContext.AllChallengesViewModel(UserMainWindow.signedInUser.ID);
            DataContext = challengesDataContext;
        }

        private void JoinChallengeButton_Checked(object sender, RoutedEventArgs e)
        {

            ToggleButton toggleButton = sender as ToggleButton;
            int selectedChallengeIndex = ChallengesListBox.Items.IndexOf(toggleButton.DataContext);

            ChallengeModel currentChallenge = (ChallengeModel) ChallengesListBox.Items[selectedChallengeIndex];

            SQLqueriesObject.JoinChallenge(UserMainWindow.signedInUser.ID, currentChallenge.ID);

            // Rrefresh Joined Challenges Cards in Home Page 
            UserMainWindow.HomePageObject.LoadJoinedChallengesCards();
        }
            
        private void JoinChallengeButton_Unchecked(object sender, RoutedEventArgs e)
        {

            ToggleButton toggleButton = sender as ToggleButton;
            int selectedChallengeIndex = ChallengesListBox.Items.IndexOf(toggleButton.DataContext);

            ChallengeModel currentChallenge = (ChallengeModel) ChallengesListBox.Items[selectedChallengeIndex];

            SQLqueriesObject.UnjoinChallenge(UserMainWindow.signedInUser.ID, currentChallenge.ID);

            // Rrefresh Joined Challenges Cards in Home Page 
            UserMainWindow.HomePageObject.LoadJoinedChallengesCards();
        }
    }

}

