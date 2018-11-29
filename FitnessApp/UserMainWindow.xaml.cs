using System.Windows;
using System.Windows.Controls;

namespace FitnessApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {
        public UserMainWindow()
        {
            InitializeComponent();
        }

        private void UserMainWindowPagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Navigate to selected Page
            switch (UserMainWindowPagesListBox.SelectedIndex)
            {
                case 0:
                    UserWindowPagesFrame.NavigationService.Navigate(UserMainWindowPages.ChallengesPage.ChallengesPageObject);
                    break;

                case 1:
                    UserWindowPagesFrame.NavigationService.Navigate(UserMainWindowPages.FitnessPlansPage.FitnessPlansPageObject);
                    break;

                case 2:
                    UserWindowPagesFrame.NavigationService.Navigate(UserMainWindowPages.CaloriesCalculatorPage.CaloriesCalculatorPageObject);
                    break;

                case 3:
                    UserWindowPagesFrame.NavigationService.Navigate(UserMainWindowPages.SettingsPage.SettingsPageObject);
                    break;
            }

            // Close Drawer after Navigation
            SideMenuDrawer.IsLeftDrawerOpen = false;
        }
    }
}
