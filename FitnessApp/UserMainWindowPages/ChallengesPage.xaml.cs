using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using FitnessApp.ViewModels;

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
            challengesDataContext.AllChallengesViewModel();
            DataContext = challengesDataContext;
        }

        private void JoinChallengeButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            //use selectedChallengeIndex + 1 as Challenge ID while using database
            ToggleButton toggleButton = sender as ToggleButton;
            selectedChallengeIndex = ChallengesListBox.Items.IndexOf(toggleButton.DataContext);
        }
    }
}
